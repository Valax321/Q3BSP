using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;

namespace OrbGarden.TrenchbroomImport.Q3BSP
{
    [ScriptedImporter(1, ".bsp", AllowCaching = true)]
    internal class Q3BSPImporter : ScriptedImporter
    {
        [SerializeField] private Material m_DevMaterial;
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            using var fs = File.OpenRead(ctx.assetPath);
            var bsp = BSPFile.Load(fs);

            var baseName = Path.GetFileNameWithoutExtension(ctx.assetPath);
            var prefabRoot = new GameObject(baseName);
            
            ctx.AddObjectToAsset(prefabRoot.name, prefabRoot);
            ctx.SetMainObject(prefabRoot);

            GenerateMeshesForMap(baseName, ctx, prefabRoot, bsp);

            for (var i = 1; i < bsp.modelsLump.models.Count; i++)
            {
                var mesh = BSPModelToMesh(baseName, i, bsp);
                ctx.AddObjectToAsset(mesh.name, mesh);
            }

            if (bsp.lightmaps.lightmaps.Count > 0)
            {
                var texArray = new Texture2DArray(128, 128, bsp.lightmaps.lightmaps.Count, TextureFormat.RGB24, false)
                {
                    name = $"{baseName}_Lightmaps"
                };
                for (var i = 0; i < bsp.lightmaps.lightmaps.Count; i++)
                {
                    GenerateLightmapTextureArray(i, texArray, bsp);
                }
            
                ctx.AddObjectToAsset(texArray.name, texArray);
            }
        }

        private void GenerateMeshesForMap(string name, AssetImportContext ctx, GameObject root, BSPFile bsp)
        {
            var usedFaces = new HashSet<BSPFace>();

            try
            {
                for (var i = 0; i < bsp.leafs.leafs.Count; i++)
                {
                    EditorUtility.DisplayProgressBar("Processing Map", $"Processing bsp {name}",
                        (float)i / bsp.leafs.leafs.Count);
                    var leaf = bsp.leafs.leafs[i];
                    var startFaceIndex = leaf.leafFace;
                    
                    var triangles = new List<int>();
                    var verts = new List<BSPVertex>();

                    for (var f = 0; f < leaf.numLeafFaces; f++)
                    {
                        var leafFace = bsp.leafFaces.leafFaces[startFaceIndex + f];
                        var face = bsp.faces.faces[leafFace.face];
                        if (usedFaces.Contains(face))
                            continue;

                        usedFaces.Add(face);

                        if (face.type is BSPFace.Type.Polygon or BSPFace.Type.Mesh)
                        {
                            Debug.Assert(face.numMeshVerts % 3 == 0);

                            for (var t = 0; t < face.numMeshVerts; t += 3)
                            {
                                var mv0 = bsp.meshVerts.meshVerts[face.meshVert + t].offset;
                                var mv1 = bsp.meshVerts.meshVerts[face.meshVert + t + 1].offset;
                                var mv2 = bsp.meshVerts.meshVerts[face.meshVert + t + 2].offset;
                        
                                var v0 = bsp.vertexes.vertexes[mv0 + face.vertex];
                                var v1 = bsp.vertexes.vertexes[mv1 + face.vertex];
                                var v2 = bsp.vertexes.vertexes[mv2 + face.vertex];

                                if (!verts.Contains(v0))
                                    verts.Add(v0);
                                if (!verts.Contains(v1))
                                    verts.Add(v1);
                                if (!verts.Contains(v2))
                                    verts.Add(v2);

                                triangles.Add(verts.IndexOf(v0));
                                triangles.Add(verts.IndexOf(v1));
                                triangles.Add(verts.IndexOf(v2));
                            }
                        }
                    }
                    
                    if (triangles.Count == 0)
                        continue;
                    
                    var vertexPositions = new List<Vector3>();
                    var vertexNormals = new List<Vector3>();
                    var vertexTexcoord0 = new List<Vector2>();
                    var vertexTexcoord1 = new List<Vector2>();
                    var vertexColor = new List<Color>();

                    foreach (var vtx in verts)
                    {
                        vertexPositions.Add(vtx.position);
                        vertexNormals.Add(vtx.normal);
                        vertexTexcoord0.Add(vtx.texCoord0);
                        vertexTexcoord1.Add(vtx.texCoord1);
                        vertexColor.Add(vtx.color);
                    }
                        
                    var mesh = new Mesh
                    {
                        name = $"{name}_Leaf{i}",
                        indexFormat = IndexFormat.UInt32 // Some leaves in wrath maps get mangled without this (maybe make it an import option?)
                    };

                    mesh.SetVertices(vertexPositions);
                    mesh.SetNormals(vertexNormals);
                    mesh.SetUVs(0, vertexTexcoord0);
                    mesh.SetUVs(1, vertexTexcoord1);
                    mesh.SetColors(vertexColor);
                    mesh.SetTriangles(triangles, 0); // TODO: need multiple index buffers to support multiple materials per leaf
            
                    mesh.RecalculateBounds(MeshUpdateFlags.Default);
                    mesh.Optimize();
                    mesh.UploadMeshData(true);
                        
                    ctx.AddObjectToAsset(mesh.name, mesh);

                    var go = new GameObject($"Leaf{i}", typeof(MeshFilter), typeof(MeshRenderer));
                    go.transform.SetParent(root.transform);
                    go.GetComponent<MeshFilter>().sharedMesh = mesh;
                    go.GetComponent<MeshRenderer>().material = m_DevMaterial;
                    
                    ctx.AddObjectToAsset(go.name, go);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private void GenerateCollisionForMap(string name, AssetImportContext ctx, GameObject root, BSPFile bsp)
        {
            for (var m = 0; m < bsp.modelsLump.models.Count; m++)
            {
                var model = bsp.modelsLump.models[m];
                if (model.numBrushes == 0)
                    continue;

                for (var b = 0; b < model.numBrushes; b++)
                {
                    var brush = bsp.brushes.brushes[model.brush + b];
                    var texture = bsp.textures.entries[brush.texture];
                }
            }
        }
        
        private Mesh BSPModelToMesh(string name, int modelIndex, BSPFile bsp)
        {
            var mesh = new Mesh
            {
                name = $"{name}_Model{modelIndex}"
            };

            var model = bsp.modelsLump.models[modelIndex];

            var triangles = new List<int>();
            var verts = new List<BSPVertex>();
            
            for (var i = 0; i < model.numFaces; i++)
            {
                var face = bsp.faces.faces[model.face + i];

                if (face.type is BSPFace.Type.Polygon or BSPFace.Type.Mesh)
                {
                    Debug.Assert(face.numMeshVerts % 3 == 0);
                    
                    for (var t = 0; t < face.numMeshVerts; t += 3)
                    {
                        var mv0 = bsp.meshVerts.meshVerts[face.meshVert + t].offset;
                        var mv1 = bsp.meshVerts.meshVerts[face.meshVert + t + 1].offset;
                        var mv2 = bsp.meshVerts.meshVerts[face.meshVert + t + 2].offset;
                        
                        var v0 = bsp.vertexes.vertexes[mv0 + face.vertex];
                        var v1 = bsp.vertexes.vertexes[mv1 + face.vertex];
                        var v2 = bsp.vertexes.vertexes[mv2 + face.vertex];

                        if (!verts.Contains(v0))
                            verts.Add(v0);
                        if (!verts.Contains(v1))
                            verts.Add(v1);
                        if (!verts.Contains(v2))
                            verts.Add(v2);

                        triangles.Add(verts.IndexOf(v0));
                        triangles.Add(verts.IndexOf(v1));
                        triangles.Add(verts.IndexOf(v2));
                    }
                }
            }

            var vertexPositions = new List<Vector3>();
            var vertexNormals = new List<Vector3>();
            var vertexTexcoord0 = new List<Vector2>();
            var vertexTexcoord1 = new List<Vector2>();
            var vertexColor = new List<Color>();

            foreach (var vtx in verts)
            {
                vertexPositions.Add(vtx.position);
                vertexNormals.Add(vtx.normal);
                vertexTexcoord0.Add(vtx.texCoord0);
                vertexTexcoord1.Add(vtx.texCoord1);
                vertexColor.Add(vtx.color);
            }

            mesh.SetVertices(vertexPositions);
            mesh.SetNormals(vertexNormals);
            mesh.SetUVs(0, vertexTexcoord0);
            mesh.SetUVs(1, vertexTexcoord1);
            mesh.SetColors(vertexColor);
            mesh.SetTriangles(triangles, 0);
            
            mesh.RecalculateBounds(MeshUpdateFlags.Default);
            mesh.Optimize();
            mesh.UploadMeshData(true);

            return mesh;
        }

        private Texture2D GenerateLightmapTexture(string name, int lightmapIndex, BSPFile bsp)
        {
            var tex = new Texture2D(128, 128, TextureFormat.RGB24, false)
            {
                name = $"{name}_Lightmap{lightmapIndex}"
            };

            var lightmap = bsp.lightmaps.lightmaps[lightmapIndex];
            
            tex.SetPixels(lightmap.map);

            return tex;
        }
        
        private void GenerateLightmapTextureArray(int lightmapIndex, Texture2DArray array, BSPFile bsp)
        {
            var lightmap = bsp.lightmaps.lightmaps[lightmapIndex];
            array.SetPixels(lightmap.map, lightmapIndex);
        }
    }
}