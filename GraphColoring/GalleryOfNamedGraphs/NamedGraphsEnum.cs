using System.ComponentModel;

namespace GraphColoring.GalleryOfNamedGraphs
{
    partial class NamedGraphs
    {
        // Named graphs
        public enum NamedGraphsEnum
        {
            // Complete graphs
            [Description("K1")]
            K1,
            [Description("K2")]
            K2,
            [Description("K3")]
            K3,
            [Description("K4")]
            K4,
            [Description("K5")]
            K5,
            [Description("K6")]
            K6,
            [Description("K7")]
            K7,
            [Description("K8")]
            K8,

            [Description("Bidiakis cube")]
            BidiakisCube,
            [Description("Bull graph")]
            BullGraph,
            [Description("Butterfly graph")]
            ButterflyGraph,
            [Description("Chvátal graph")]
            ChvatalGraph,
            [Description("Diamond graph")]
            DiamondGraph,
            [Description("Dürer graph")]
            DurerGraph,
            [Description("Errera graph")]
            ErreraGraph,
            [Description("Franklin graph")]
            FranklinGraph,
            [Description("Frucht graph")]
            FruchtGraph,
            [Description("Goldner–Harary graph")]
            Goldner_HararyGraph,
            [Description("Golomb graph")]
            GolombGraph,
            [Description("Grötzsch graph")]
            GrotzschGraph,
            [Description("Herschel graph")]
            HerschelGraph,
            [Description("Hoffman graph")]
            HoffmanGraph,
            [Description("Markström graph")]
            MarkstromGraph,
            [Description("McGee graph")]
            McGeeGraph,
            [Description("Moser spindle")]
            MoserSpindle,
            [Description("Poussin graph")]
            RobertsonGraph,
            [Description("Sousselier graph")]
            PoussinGraph,
            [Description("Robertson graph")]
            SousselierGraph,
            [Description("Tutte's fragment")]
            TuttesFragment,
            [Description("Wagner graph")]
            WagnerGraph,

            // Complete bipartite graphs
            [Description("K2,3")]
            K2_3,
            [Description("K2,4")]
            K2_4,
            [Description("K3,3")]
            K3_3,
            [Description("K3,4")]
            K3_4,

            // Cycle graphs
            [Description("C3")]
            C3,
            [Description("C4")]
            C4,
            [Description("C5")]
            C5,
            [Description("C6")]
            C6,

            // Friendship graphs
            [Description("F2")]
            F2,
            [Description("F3")]
            F3,
            [Description("F4")]
            F4,

            // Fullerene graphs
            [Description("20-fullerene (dodecahedral graph)")]
            _20_fullerene,
            [Description("24-fullerene (Hexagonal truncated trapezohedron graph)")]
            _24_fullerene,

            // Platonic solids
            [Description("Cube")]
            Cube,
            [Description("Octahedron")]
            Octahedron,
            [Description("Dodecahedron")]
            Dodecahedron,
            [Description("Icosahedron")]
            Icosahedron,

            // Semi-symmetric graphs
            [Description("Folkman graph")]
            FolkmanGraph,

            // Snarks
            [Description("Blanuša snark")]
            BlanusaSnark,
            [Description("Flower snark")]
            FlowerSnark,
            [Description("Loupekine snark")]
            LoupekineSnark,
            [Description("Tietze graph")]
            TietzesGraph,

            // Star graphs
            [Description("S3")]
            S3,
            [Description("S4")]
            S4,
            [Description("S5")]
            S5,
            [Description("S6")]
            S6,

            // Strongly regular graphs
            [Description("Clebsch graph")]
            ClebschGraph,
            [Description("Petersen graph")]
            PetersenGraph,
            [Description("Paley graph of order 13")]
            PaleyGraph,

            // Symmetric graphs
            [Description("Heawood graph")]
            HeawoodGraph,
            [Description("Möbius–Kantor graph")]
            Mobius_KantorGraph,
            [Description("Pappus graph")]
            PappusGraph,
            [Description("Desargues graph")]
            DesarguesGraph,

            // Truncated solids
            [Description("Truncated tetrahedron")]
            TruncatedTetrahedron,
            [Description("Truncated cube")]
            TruncatedCube,

            // Wheel graphs
            [Description("W4")]
            W4,
            [Description("W5")]
            W5,
            [Description("W6")]
            W6,
            [Description("W7")]
            W7,
            [Description("W8")]
            W8,
            [Description("W9")]
            W9
        };
        /*
        public static Dictionary<NamedGraphsEnum, string> WCMNamedGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.BidiakisCube, "Bidiakis cube" },
            { NamedGraphsEnum.BullGraph, "Bull graph" },
            { NamedGraphsEnum.ButterflyGraph, "Butterfly graph" },
            { NamedGraphsEnum.ChvatalGraph, "Chvátal graph" },
            { NamedGraphsEnum.DiamondGraph, "Diamond graph" },
            { NamedGraphsEnum.DurerGraph, "Dürer graph" },
            { NamedGraphsEnum.ErreraGraph, "Errera graph" },
            { NamedGraphsEnum.FranklinGraph, "Franklin graph" },
            { NamedGraphsEnum.FruchtGraph, "Frucht graph" },
            { NamedGraphsEnum.Goldner_HararyGraph, "Goldner–Harary graph" },
            { NamedGraphsEnum.GolombGraph, "Golomb graph" },
            { NamedGraphsEnum.GrotzschGraph, "Grötzsch graph" },
            { NamedGraphsEnum.HerschelGraph, "Herschel graph" },
            { NamedGraphsEnum.HoffmanGraph, "Hoffman graph" },
            { NamedGraphsEnum.MarkstromGraph, "Markström graph" },
            { NamedGraphsEnum.McGeeGraph, "McGee graph" },
            { NamedGraphsEnum.MoserSpindle, "Moser spindle" },
            { NamedGraphsEnum.PoussinGraph, "Poussin graph" },
            { NamedGraphsEnum.SousselierGraph, "Sousselier graph" },
            { NamedGraphsEnum.RobertsonGraph, "Robertson graph" },
            { NamedGraphsEnum.TuttesFragment, "Tutte's fragment" },
            { NamedGraphsEnum.WagnerGraph, "Wagner graph" },

            // Complete bipartite graphs
            { NamedGraphsEnum.K2_3, "K2,3" },
            { NamedGraphsEnum.K2_4, "K2,4" },
            { NamedGraphsEnum.K3_3, "K3,3" },
            { NamedGraphsEnum.K3_4, "K3,4" },

            // Complete graphs
            { NamedGraphsEnum.K1, "K1" },
            { NamedGraphsEnum.K2, "K2" },
            { NamedGraphsEnum.K3, "K3" },
            { NamedGraphsEnum.K4, "K4" },
            { NamedGraphsEnum.K5, "K5" },
            { NamedGraphsEnum.K6, "K6" },
            { NamedGraphsEnum.K7, "K7" },
            { NamedGraphsEnum.K8, "K8" },

            // Cycle graphs
            { NamedGraphsEnum.C3, "C3" },
            { NamedGraphsEnum.C4, "C4" },
            { NamedGraphsEnum.C5, "C5" },
            { NamedGraphsEnum.C6, "C6" },

            // Friendship graphs
            { NamedGraphsEnum.F2, "F2" },
            { NamedGraphsEnum.F3, "F3" },
            { NamedGraphsEnum.F4, "F4" },

            // Fullerene graphs
            { NamedGraphsEnum._20_fullerene, "20-fullerene (dodecahedral graph)" },
            { NamedGraphsEnum._24_fullerene, "24-fullerene (Hexagonal truncated trapezohedron graph)" },

            // Platonic solids
            { NamedGraphsEnum.Cube, "Cube" },
            { NamedGraphsEnum.Octahedron, "Octahedron" },
            { NamedGraphsEnum.Dodecahedron, "Dodecahedron" },
            { NamedGraphsEnum.Icosahedron, "Icosahedron" },

            // Semi-symmetric graphs
            { NamedGraphsEnum.FolkmanGraph, "Folkman graph" },

            // Snarks
            { NamedGraphsEnum.BlanusaSnark, "Blanuša snark" },
            { NamedGraphsEnum.FlowerSnark, "Flower snark" },
            { NamedGraphsEnum.LoupekineSnark, "Loupekine snark " },
            { NamedGraphsEnum.TietzesGraph, "Tietze graph" },

            // Star graphs
            { NamedGraphsEnum.S3, "S3" },
            { NamedGraphsEnum.S4, "S4" },
            { NamedGraphsEnum.S5, "S5" },
            { NamedGraphsEnum.S6, "S6" },

            // Strongly regular graphs
            { NamedGraphsEnum.ClebschGraph, "Clebsch graph" },
            { NamedGraphsEnum.PetersenGraph, "Petersen graph" },
            { NamedGraphsEnum.PaleyGraph, "Paley graph of order 13" },

            // Symmetric graphs
            { NamedGraphsEnum.HeawoodGraph, "Heawood graph" },
            { NamedGraphsEnum.Mobius_KantorGraph, "Möbius–Kantor graph" },
            { NamedGraphsEnum.PappusGraph, "Pappus graph" },
            { NamedGraphsEnum.DesarguesGraph, "Desargues graph" },

            // Truncated solids
            { NamedGraphsEnum.TruncatedTetrahedron, "Truncated tetrahedron" },
            { NamedGraphsEnum.TruncatedCube, "Truncated cube" },

            // Wheel graphs
            { NamedGraphsEnum.W4, "W4" },
            { NamedGraphsEnum.W5, "W5" },
            { NamedGraphsEnum.W6, "W6" },
            { NamedGraphsEnum.W7, "W7" },
            { NamedGraphsEnum.W8, "W8" },
            { NamedGraphsEnum.W9, "W9" }
        };
        */
    }
}
