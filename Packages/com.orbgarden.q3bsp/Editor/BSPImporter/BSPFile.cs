using System.IO;
using System.Text;

namespace OrbGarden.Q3BSP.BSPImporter
{
    internal class BSPFile
    {
        public const uint BSPFileMagic = 'I' + ('B' << 8) + ('S' << 16) + ('P' << 24);
        public const int BSPVersion = 0x2e;
        
        public BSPHeader header { get; }
        
        public EntitiesLump entities { get; }
        public TexturesLump textures { get; }
        public PlanesLump planes { get; }
        public NodesLump nodes { get; }
        public LeafsLump leafs { get; }
        public LeafFacesLump leafFaces { get; }
        public LeafBrushLump leafBrushes { get; }
        public ModelsLump modelsLump { get; }
        public BrushesLump brushes { get; }
        public BrushSidesLump brushSides { get; }
        public VertexesLump vertexes { get; }
        public MeshVertsLump meshVerts { get; }
        public EffectsLump effects { get; }
        public FacesLump faces { get; }
        public LightmapsLump lightmaps { get; }
        public LightVolsLump lightVols { get; }
        public VisdataLump visData { get; }
        
        public static BSPFile Load(Stream stream)
        {
            return new BSPFile(stream);
        }

        private BSPFile(Stream stream)
        {
            using var br = new BinaryReader(stream, Encoding.Default, true);

            header = new BSPHeader(br);
            if (header.magic != BSPFileMagic)
                throw new BSPParseException("Invalid IBSP magic");
            if (header.version != BSPVersion)
                throw new BSPParseException($"Unknown BSP version {header.version}");

            entities = new EntitiesLump(br, ref header.directories[0]);
            textures = new TexturesLump(br, ref header.directories[1]);
            planes = new PlanesLump(br, ref header.directories[2]);
            nodes = new NodesLump(br, ref header.directories[3]);
            leafs = new LeafsLump(br, ref header.directories[4]);
            leafFaces = new LeafFacesLump(br, ref header.directories[5]);
            leafBrushes = new LeafBrushLump(br, ref header.directories[6]);
            modelsLump = new ModelsLump(br, ref header.directories[7]);
            brushes = new BrushesLump(br, ref header.directories[8]);
            brushSides = new BrushSidesLump(br, ref header.directories[9]);
            vertexes = new VertexesLump(br, ref header.directories[10]);
            meshVerts = new MeshVertsLump(br, ref header.directories[11]);
            effects = new EffectsLump(br, ref header.directories[12]);
            faces = new FacesLump(br, ref header.directories[13]);
            lightmaps = new LightmapsLump(br, ref header.directories[14]);
            //lightVols = new LightVolsLump(br, ref header.directories[15]);
            visData = new VisdataLump(br, ref header.directories[16]);
        }
    }
}