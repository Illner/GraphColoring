using System;
using System.Collections.Generic;

namespace GraphColoring.GalleryOfNamedGraphs
{
    partial class NamedGraphs
    {
        // Variable
        // Named graphs
        static Dictionary<NamedGraphsEnum, string> namedGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.BidiakisCube, NamedGraphsResources.NamedGraphsResource.BidiakisCube },
            { NamedGraphsEnum.BullGraph, NamedGraphsResources.NamedGraphsResource.BullGraph },
            { NamedGraphsEnum.ButterflyGraph, NamedGraphsResources.NamedGraphsResource.ButterflyGraph },
            { NamedGraphsEnum.ChvatalGraph, NamedGraphsResources.NamedGraphsResource.ChvatalGraph },
            { NamedGraphsEnum.DiamondGraph, NamedGraphsResources.NamedGraphsResource.DiamondGraph },
            { NamedGraphsEnum.DurerGraph, NamedGraphsResources.NamedGraphsResource.DurerGraph },
            { NamedGraphsEnum.ErreraGraph, NamedGraphsResources.NamedGraphsResource.ErreraGraph },
            { NamedGraphsEnum.FranklinGraph, NamedGraphsResources.NamedGraphsResource.FranklinGraph },
            { NamedGraphsEnum.FruchtGraph, NamedGraphsResources.NamedGraphsResource.FruchtGraph },
            { NamedGraphsEnum.Goldner_HararyGraph, NamedGraphsResources.NamedGraphsResource.Goldner_HararyGraph },
            { NamedGraphsEnum.GolombGraph, NamedGraphsResources.NamedGraphsResource.GolombGraph },
            { NamedGraphsEnum.GrotzschGraph, NamedGraphsResources.NamedGraphsResource.GrotzschGraph },
            { NamedGraphsEnum.HerschelGraph, NamedGraphsResources.NamedGraphsResource.HerschelGraph },
            { NamedGraphsEnum.HoffmanGraph, NamedGraphsResources.NamedGraphsResource.HoffmanGraph },
            { NamedGraphsEnum.MarkstromGraph, NamedGraphsResources.NamedGraphsResource.MarkstromGraph },
            { NamedGraphsEnum.McGeeGraph, NamedGraphsResources.NamedGraphsResource.McGeeGraph },
            { NamedGraphsEnum.MoserSpindle, NamedGraphsResources.NamedGraphsResource.MoserSpindle },
            { NamedGraphsEnum.PoussinGraph, NamedGraphsResources.NamedGraphsResource.PoussinGraph },
            { NamedGraphsEnum.SousselierGraph, NamedGraphsResources.NamedGraphsResource.SousselierGraph },
            { NamedGraphsEnum.RobertsonGraph, NamedGraphsResources.NamedGraphsResource.RobertsonGraph },
            { NamedGraphsEnum.TuttesFragment, NamedGraphsResources.NamedGraphsResource.TuttesFragment },
            { NamedGraphsEnum.WagnerGraph, NamedGraphsResources.NamedGraphsResource.WagnerGraph }
        };

        // Complete bipartite graphs
        static Dictionary<NamedGraphsEnum, string> completeBipartiteGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.K2_3, NamedGraphsResources.CompleteBipartiteGraphsResource.K2_3 },
            { NamedGraphsEnum.K2_4, NamedGraphsResources.CompleteBipartiteGraphsResource.K2_4 },
            { NamedGraphsEnum.K3_3, NamedGraphsResources.CompleteBipartiteGraphsResource.K3_3 },
            { NamedGraphsEnum.K3_4, NamedGraphsResources.CompleteBipartiteGraphsResource.K3_4 }
        };

        // Complete graphs
        static Dictionary<NamedGraphsEnum, string> completeGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.K1, NamedGraphsResources.CompleteGraphsResource.K1 },
            { NamedGraphsEnum.K2, NamedGraphsResources.CompleteGraphsResource.K2 },
            { NamedGraphsEnum.K3, NamedGraphsResources.CompleteGraphsResource.K3 },
            { NamedGraphsEnum.K4, NamedGraphsResources.CompleteGraphsResource.K4 },
            { NamedGraphsEnum.K5, NamedGraphsResources.CompleteGraphsResource.K5 },
            { NamedGraphsEnum.K6, NamedGraphsResources.CompleteGraphsResource.K6 },
            { NamedGraphsEnum.K7, NamedGraphsResources.CompleteGraphsResource.K7 },
            { NamedGraphsEnum.K8, NamedGraphsResources.CompleteGraphsResource.K8 },
        };

        // Path graphs
        static Dictionary<NamedGraphsEnum, string> pathGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.P3, NamedGraphsResources.PathGraphsResources.P3 },
            { NamedGraphsEnum.P4, NamedGraphsResources.PathGraphsResources.P4 },
            { NamedGraphsEnum.P5, NamedGraphsResources.PathGraphsResources.P5 },
            { NamedGraphsEnum.P6, NamedGraphsResources.PathGraphsResources.P6 },
            { NamedGraphsEnum.P7, NamedGraphsResources.PathGraphsResources.P7 },
            { NamedGraphsEnum.P8, NamedGraphsResources.PathGraphsResources.P8 },
            { NamedGraphsEnum.P9, NamedGraphsResources.PathGraphsResources.P9 },
        };

        // Cycle graphs
        static Dictionary<NamedGraphsEnum, string> cycleGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.C3, NamedGraphsResources.CycleGraphsResource.C3 },
            { NamedGraphsEnum.C4, NamedGraphsResources.CycleGraphsResource.C4 },
            { NamedGraphsEnum.C5, NamedGraphsResources.CycleGraphsResource.C5 },
            { NamedGraphsEnum.C6, NamedGraphsResources.CycleGraphsResource.C6 },
        };

        // Friendship graphs
        static Dictionary<NamedGraphsEnum, string> friendshipGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.F2, NamedGraphsResources.FriendshipGraphsResource.F2 },
            { NamedGraphsEnum.F3, NamedGraphsResources.FriendshipGraphsResource.F3 },
            { NamedGraphsEnum.F4, NamedGraphsResources.FriendshipGraphsResource.F4 }
        };

        // Fullerene graphs
        static Dictionary<NamedGraphsEnum, string> fullereneGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum._20_fullerene, NamedGraphsResources.FullereneGraphsResource._20_fullerene },
            { NamedGraphsEnum._24_fullerene, NamedGraphsResources.FullereneGraphsResource._24_fullerene }
        };

        // Platonic solids
        static Dictionary<NamedGraphsEnum, string> platonicSolidsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.Cube, NamedGraphsResources.PlatonicSolidsResource.Cube },
            { NamedGraphsEnum.Octahedron, NamedGraphsResources.PlatonicSolidsResource.Octahedron },
            { NamedGraphsEnum.Dodecahedron, NamedGraphsResources.PlatonicSolidsResource.Dodecahedron },
            { NamedGraphsEnum.Icosahedron, NamedGraphsResources.PlatonicSolidsResource.Icosahedron }
        };

        // Semi-symmetric graphs
        static Dictionary<NamedGraphsEnum, string> semiSymmetricGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.FolkmanGraph, NamedGraphsResources.SemiSymmetricGraphsResource.FolkmanGraph }
        };

        // Snarks
        static Dictionary<NamedGraphsEnum, string> snarksDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.BlanusaSnark, NamedGraphsResources.SnarksResource.BlanusaSnark },
            { NamedGraphsEnum.FlowerSnark, NamedGraphsResources.SnarksResource.FlowerSnark },
            { NamedGraphsEnum.LoupekineSnark, NamedGraphsResources.SnarksResource.LoupekineSnark },
            { NamedGraphsEnum.TietzesGraph, NamedGraphsResources.SnarksResource.TietzesGraph }
        };

        // Star graphs
        static Dictionary<NamedGraphsEnum, string> starGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.S3, NamedGraphsResources.StarGraphsResource.S3 },
            { NamedGraphsEnum.S4, NamedGraphsResources.StarGraphsResource.S4 },
            { NamedGraphsEnum.S5, NamedGraphsResources.StarGraphsResource.S5 },
            { NamedGraphsEnum.S6, NamedGraphsResources.StarGraphsResource.S6 },
        };

        // Strongly regular graphs
        static Dictionary<NamedGraphsEnum, string> stronglyEegularGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.ClebschGraph, NamedGraphsResources.StronglyRegularGraphsResource.ClebschGraph },
            { NamedGraphsEnum.PetersenGraph, NamedGraphsResources.StronglyRegularGraphsResource.PetersenGraph },
            { NamedGraphsEnum.PaleyGraph, NamedGraphsResources.StronglyRegularGraphsResource.PaleyGraph }
        };

        // Symmetric graphs
        static Dictionary<NamedGraphsEnum, string> symmetricGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.HeawoodGraph, NamedGraphsResources.SymmetricGraphsResource.HeawoodGraph },
            { NamedGraphsEnum.Mobius_KantorGraph, NamedGraphsResources.SymmetricGraphsResource.Mobius_KantorGraph },
            { NamedGraphsEnum.PappusGraph, NamedGraphsResources.SymmetricGraphsResource.PappusGraph },
            { NamedGraphsEnum.DesarguesGraph, NamedGraphsResources.SymmetricGraphsResource.DesarguesGraph }
        };

        // Truncated solids
        static Dictionary<NamedGraphsEnum, string> truncatedSolidsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.TruncatedTetrahedron, NamedGraphsResources.TruncatedSolidsResource.TruncatedTetrahedron },
            { NamedGraphsEnum.TruncatedCube, NamedGraphsResources.TruncatedSolidsResource.TruncatedCube }
        };

        // Wheel graphs
        static Dictionary<NamedGraphsEnum, string> wheelGraphsDictionary = new Dictionary<NamedGraphsEnum, string>()
        {
            { NamedGraphsEnum.W4, NamedGraphsResources.WheelGraphsResource.W4 },
            { NamedGraphsEnum.W5, NamedGraphsResources.WheelGraphsResource.W5 },
            { NamedGraphsEnum.W6, NamedGraphsResources.WheelGraphsResource.W6 },
            { NamedGraphsEnum.W7, NamedGraphsResources.WheelGraphsResource.W7 },
            { NamedGraphsEnum.W8, NamedGraphsResources.WheelGraphsResource.W8 },
            { NamedGraphsEnum.W9, NamedGraphsResources.WheelGraphsResource.W9 },
        };

        // Main list
        public static List<Dictionary<NamedGraphsEnum, string>> namedGraphsList = new List<Dictionary<NamedGraphsEnum, string>>()
        {
            completeGraphsDictionary,
            pathGraphsDictionary,
            namedGraphsDictionary,
            completeBipartiteGraphsDictionary,
            cycleGraphsDictionary,
            friendshipGraphsDictionary,
            fullereneGraphsDictionary,
            platonicSolidsDictionary,
            semiSymmetricGraphsDictionary,
            snarksDictionary,
            starGraphsDictionary,
            stronglyEegularGraphsDictionary,
            symmetricGraphsDictionary,
            truncatedSolidsDictionary,
            wheelGraphsDictionary
        };
    }
}
