using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using ReqManager.Entities.Project;
using ReqManager.Entities.Requirement;
using ReqManager.Services.Documents.Interfaces;
using ReqManager.Services.Project.Interfaces;
using ReqManager.Services.Requirements.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReqManager.Services.Documents.Classes
{
    public class RequirementDocumentService : IRequirementDocumentService
    {
        private IRequirementService requirementService { get; set; }
        private IProjectService projectService { get; set; }
        private IStakeholdersProjectService stakeholderProjectService { get; set; }

        public RequirementDocumentService(
            IRequirementService requirementService,
            IProjectService projectService,
            IStakeholdersProjectService stakeholderProjectService)
        {
            this.stakeholderProjectService = stakeholderProjectService;
            this.projectService = projectService;
            this.requirementService = requirementService;
        }

        public byte[] printRequirement(int RequirementID)
        {
            try
            {
                RequirementEntity requirement = requirementService.get(RequirementID);

                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4, 40, 40, 40, 80))
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();

                            using (var html = new StringReader(
                                getProjectHeader(requirement.Project) + 
                                getRequirementHtml(requirement) +
                                getFooter(requirement.Project)))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, html);
                            }

                            doc.Close();
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] printDocumentRequirementProject(int ProjectID, int RequirementTypeID)
        {
            try
            {
                List<RequirementEntity> requirements =
                    requirementService.getRequirementsByProject(ProjectID).
                    Where(r => r.RequirementTypeID.Equals(RequirementTypeID)).ToList();

                ProjectEntity project = projectService.get(ProjectID);

                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4, 40, 40, 40, 80))
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();

                            string body = string.Empty;
                            requirements.ForEach(r => body += getRequirementHtml(r));

                            using (var html = new StringReader(getProjectHeader(project) + body + getFooter(project)))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, html);
                            }

                            doc.Close();
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getProjectHeader(ProjectEntity project)
        {
            return @"<hr />
                    <h2>Requirement Document</h2>
                    <p><strong>Project</strong>: " + project.description + @"</p>
                    <p><strong>Code</strong>: " + project.code + @"</p>
                    <p><strong>Phase</strong>: " + project.ProjectPhases.description + @"</p>
                    <p><strong>Environmental Information</strong>: " + project.environmentalInformation + @"</p>
                    <p><strong>Management Information</strong>: " + project.managementInformation + @"</p>
                    <p><strong>Start Date</strong>: " + project.startDate.ToShortDateString() + @"</p>
                    <p><strong>End Date</strong>: " + project.endDate.ToShortDateString() + @"</p>
                    <br />
                    <hr />
                    <h2>Project Requirements</h2>";
        }

        private string getRequirementHtml(RequirementEntity requirement)
        {
            return @"<p style=""padding - left: 30px; ""><strong>Code</strong>: " + requirement.code + @"</p>
   < p style = ""padding-left: 30px;"" >< strong > Version Number </ strong >: " + requirement.versionNumber + @"</ p >
             < p style = ""padding-left: 30px;"" >< strong > Title </ strong >: " + requirement.title + @"</ p >
                       < p style = ""padding-left: 30px;"" >< strong > Status </ strong >: " + requirement.RequirementStatus.description + @"</ p >
                                 < p style = ""padding-left: 30px;"" >< strong > Importance </ strong >: " + requirement.Importance.description + @"</ p >
                                           < p style = ""padding-left: 30px;"" >< strong > Type </ strong >: " + requirement.RequirementType.description + @"</ p >
                                                     < p style = ""padding-left: 30px;"" >< strong > SubType </ strong >: " + requirement.RequirementSubType.description + @"</ p >
                                                               < p style = ""padding-left: 30px;"" >< strong > Description </ strong >: " + requirement.description + @"</ p >
                                                                         < hr />
                                                                         < p > &nbsp;</ p > ";
        }

        private string getFooter(ProjectEntity project)
        {
            try
            {
                string footer = @"<br/>
                                <h2>Project Stakeholders Agreement</h2>
                                <br/>";
                var stakeholders = stakeholderProjectService.getStakeholderByProject(project.ProjectID).ToList();
                foreach (var item in stakeholders)
                {
                    footer+= @"<h3 style=""text-align:center;"">__________________________________________________</h3>
                                <h3 style=""text-align:center;"">" + item.Stakeholders.DisplayName + @"</h3>";
                }

                return footer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
