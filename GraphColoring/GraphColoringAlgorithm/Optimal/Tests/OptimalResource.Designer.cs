﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GraphColoring.GraphColoringAlgorithm.Optimal.Tests {
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
    internal class OptimalResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal OptimalResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GraphColoring.GraphColoringAlgorithm.Optimal.Tests.OptimalResource", typeof(OptimalResource).Assembly);
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
        ///Graph name: Name
        ///Number of vertices: 6
        ///
        ///GRAPH
        ///0 0 1 0 1 0
        ///0 0 1 1 1 1
        ///1 1 0 0 1 1
        ///0 1 0 0 0 1
        ///1 1 1 0 0 1
        ///0 1 1 1 1 0
        ///
        ///COLORED GRAPH.
        /// </summary>
        internal static string optimal1 {
            get {
                return ResourceManager.GetString("optimal1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Name
        ///Number of vertices: 7
        ///
        ///GRAPH
        ///0 1 1 1 1 1 0
        ///1 0 1 1 1 1 1
        ///1 1 0 1 1 1 0
        ///1 1 1 0 0 1 1
        ///1 1 1 0 0 1 1
        ///1 1 1 1 1 0 1
        ///0 1 0 1 1 1 0
        ///
        ///COLORED GRAPH.
        /// </summary>
        internal static string optimal2 {
            get {
                return ResourceManager.GetString("optimal2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Name
        ///Number of vertices: 0
        ///
        ///GRAPH
        ///
        ///COLORED GRAPH.
        /// </summary>
        internal static string optimal3 {
            get {
                return ResourceManager.GetString("optimal3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Name
        ///Number of vertices: 1
        ///
        ///GRAPH
        ///0
        ///
        ///COLORED GRAPH.
        /// </summary>
        internal static string optimal4 {
            get {
                return ResourceManager.GetString("optimal4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Graph coloring. Graph representation: adjacencyMatrix
        ///
        ///Graph name: Name
        ///Number of vertices: 6
        ///
        ///GRAPH
        ///0 1 1 0 0 0
        ///1 0 1 0 0 0
        ///1 1 0 0 0 0
        ///0 0 0 0 1 1
        ///0 0 0 1 0 1
        ///0 0 0 1 1 0
        ///
        ///COLORED GRAPH.
        /// </summary>
        internal static string optimal5 {
            get {
                return ResourceManager.GetString("optimal5", resourceCulture);
            }
        }
    }
}
