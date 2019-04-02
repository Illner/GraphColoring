﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GraphColoring.Graph.GraphProperty.Tests {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ChordalResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ChordalResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GraphColoring.Graph.GraphProperty.Tests.ChordalResource", typeof(ChordalResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Chordal
        ///Number of vertices: 8
        ///
        ///GRAPH
        ///0 1 1 1 0 0 0 0
        ///1 0 1 1 1 0 0 1
        ///1 1 0 1 0 1 1 0
        ///1 1 1 0 1 1 1 0
        ///0 1 0 1 0 0 0 1
        ///0 0 1 1 0 0 1 0
        ///0 0 1 1 0 1 0 0
        ///0 1 0 0 1 0 0 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal1 {
            get {
                return ResourceManager.GetString("graphChordal1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Chordal
        ///Number of vertices: 8
        ///
        ///GRAPH
        ///0 0 0 0 1 0 1 1
        ///0 0 0 1 0 0 0 0
        ///0 0 0 0 0 0 0 1
        ///0 1 0 0 0 0 0 1
        ///1 0 0 0 0 0 1 1
        ///0 0 0 0 0 0 1 1
        ///1 0 0 0 1 1 0 1
        ///1 0 1 1 1 1 1 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal2 {
            get {
                return ResourceManager.GetString("graphChordal2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: NonChordal
        ///Number of vertices: 8
        ///
        ///GRAPH
        ///0 0 0 0 1 1 1 1
        ///0 0 1 0 0 1 1 1
        ///0 1 0 1 0 0 0 1
        ///0 0 1 0 1 0 0 0
        ///1 0 0 1 0 0 0 0
        ///1 1 0 0 0 0 1 0
        ///1 1 0 0 0 1 0 0
        ///1 1 1 0 0 0 0 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal3 {
            get {
                return ResourceManager.GetString("graphChordal3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: NonChordal
        ///Number of vertices: 8
        ///
        ///GRAPH
        ///0 0 0 1 0 0 0 1
        ///0 0 1 0 1 0 0 1
        ///0 1 0 1 0 0 0 0
        ///1 0 1 0 0 0 0 0
        ///0 1 0 0 0 1 0 0
        ///0 0 0 0 1 0 1 0
        ///0 0 0 0 0 1 0 1
        ///1 1 0 0 0 0 1 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal4 {
            get {
                return ResourceManager.GetString("graphChordal4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: NonChordal
        ///Number of vertices: 10
        ///
        ///GRAPH
        ///0 1 1 1 0 0 0 1 1 0
        ///1 0 1 1 0 0 0 1 1 0
        ///1 1 0 0 1 0 0 1 1 0
        ///1 1 0 0 1 0 0 1 1 0
        ///0 0 1 1 0 0 0 1 1 0
        ///0 0 0 0 0 0 0 1 1 0
        ///0 0 0 0 0 0 0 0 0 1
        ///1 1 1 1 1 1 0 0 0 1
        ///1 1 1 1 1 1 0 0 0 1
        ///0 0 0 0 0 0 1 1 1 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal5 {
            get {
                return ResourceManager.GetString("graphChordal5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Chordal
        ///Number of vertices: 1
        ///
        ///GRAPH
        ///0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal6 {
            get {
                return ResourceManager.GetString("graphChordal6", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Chordal
        ///Number of vertices: 9
        ///
        ///GRAPH
        ///0 1 1 0 0 0 0 0 0
        ///1 0 1 0 0 0 0 0 0
        ///1 1 0 0 0 0 0 0 0
        ///0 0 0 0 1 1 0 0 0
        ///0 0 0 1 0 1 0 0 0
        ///0 0 0 1 1 0 0 0 0
        ///0 0 0 0 0 0 0 1 1
        ///0 0 0 0 0 0 1 0 1
        ///0 0 0 0 0 0 1 1 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal7 {
            get {
                return ResourceManager.GetString("graphChordal7", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Chordal
        ///Number of vertices: 9
        ///
        ///GRAPH
        ///0 1 1 1 1 1 1 1 1
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///1 0 0 0 0 0 0 0 0
        ///
        ///COLORED GRAPH
        ///Chromatic number: 3
        ///Used algorithm: optimal
        ///0 -&gt; 0
        ///1 -&gt; 0
        ///2 -&gt; 0
        ///3 -&gt; 1
        ///4 -&gt; 0
        ///5 -&gt; 1
        ///6 -&gt; 2
        ///7 -&gt; 1
        ///8 -&gt; 2
        ///9 -&gt; 3.
        /// </summary>
        internal static string graphChordal8 {
            get {
                return ResourceManager.GetString("graphChordal8", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: NonChordal
        ///Number of vertices: 14
        ///
        ///GRAPH
        ///0 1 0 0 0 0 0 0 0 0 0 0 0 0
        ///1 0 1 0 0 0 0 0 0 0 0 0 0 0
        ///0 1 0 1 0 0 0 0 0 0 0 0 0 0
        ///0 0 1 0 1 0 0 0 0 0 0 0 0 0
        ///0 0 0 1 0 1 0 0 0 0 0 0 0 0
        ///0 0 0 0 1 0 1 0 1 0 0 0 0 0
        ///0 0 0 0 0 1 0 1 0 0 0 0 0 0
        ///0 0 0 0 0 0 1 0 0 0 0 0 0 0
        ///0 0 0 0 0 1 0 0 0 0 0 0 0 0
        ///0 0 0 0 0 0 0 0 0 0 1 1 0 0
        ///0 0 0 0 0 0 0 0 0 1 0 0 1 0
        ///0 0 0 0 0 0 0 0 0 1 0 0 1 0
        ///0 0 0 0 0 0 0 0 0 0 1 1 0 0
        ///0 0 0 0 0 0 0 0 0 0 0 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string graphChordal9 {
            get {
                return ResourceManager.GetString("graphChordal9", resourceCulture);
            }
        }
    }
}
