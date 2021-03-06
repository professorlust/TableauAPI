﻿using System;
using System.Text;
using System.Xml;
using TableauAPI.FilesLogging;

namespace TableauAPI.ServerData
{
    /// <summary>
    /// Information about a Project in a Server's site
    /// </summary>
    public class SiteProject : IHasSiteItemId
    {
        /// <summary>
        /// ID of the Project
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Name of the Project
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Description of the Project
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Any developer/diagnostic notes we want to indicate
        /// </summary>
        public readonly string DeveloperNotes;

        /// <summary>
        /// Creates an instance of a SiteProject from XML returned by the Tableau server
        /// </summary>
        /// <param name="projectNode"></param>
        public SiteProject(XmlNode projectNode)
        {
            var sbDevNotes = new StringBuilder();

            if(projectNode.Name.ToLower() != "project")
            {
                AppDiagnostics.Assert(false, "Not a project");
                throw new Exception("Unexpected content - not project");
            }

            Id = projectNode.Attributes?["id"].Value;
            Name = projectNode.Attributes?["name"].Value;

            var descriptionNode = projectNode.Attributes?["description"];
            if(descriptionNode != null)
            {
                Description = descriptionNode.Value;
            }
            else
            {
                Description = "";
                sbDevNotes.AppendLine("Project is missing description attribute");
            }

            DeveloperNotes = sbDevNotes.ToString();
        }

        /// <summary>
        /// Creates an instance of a SiteProject with name and ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Id"></param>
        public SiteProject(string name, string Id)
        {
            Name = name;
            this.Id = Id;
        }

        /// <summary>
        /// Returns Name and ID of Projec.t
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Project: " + Name + "/" + Id;
        }

        /// <summary>
        /// Project ID
        /// </summary>
        string IHasSiteItemId.Id => Id;
    }
}
