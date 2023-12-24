using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsToolTip
    {
        public clsToolTip() { }

        public clsToolTip(string toolTipFilesFolder) 
        {
            this.toolTipFilesFolder = toolTipFilesFolder;
        }

        public clsToolTip(string toolTipFilesFolder, string templateFile, string jsFile)
        {
            this.templateFile = templateFile;
            this.jsFile = jsFile;
            this.toolTipFilesFolder = ToolTipFilesFolder;
        }

        private ArrayList texts = new ArrayList();
        private ArrayList controls = new ArrayList();
        private ArrayList literals = new ArrayList();

        private string templateFile = "ToolTipTemplate.config";
        private string jsFile = "tooltip.js";
        private string toolTipFilesFolder = "tooltip_files";

        public string ToolTipFilesFolder
        {
            get
            {
                return toolTipFilesFolder;
            }
            set
            {
                toolTipFilesFolder = value;
            }
        }

        public void Add(WebControl control, Literal literal, string text)
        {
            controls.Add(control);
            texts.Add(text);
            literals.Add(literal);
        }

        private string template = string.Empty;

        public string Template
        {
            get
            {
                if (template.Length == 0)
                {
                    template = ReadFileBody(HttpContext.Current.Server.MapPath(toolTipFilesFolder + "/" + templateFile));
                }
                return template;
            }
        }

        public void Build()
        {
            for (int i = 0; i < controls.Count; i++)
            {

                string guid = Guid.NewGuid().ToString();
                string html = Template;
                html = html.Replace("[TABLEID]", guid);
                html = html.Replace("[TEXT]", texts[i].ToString());
                html = html.Replace("[tooltip_files]", ToolTipFilesFolder);

                Literal literal = (Literal)literals[i];
                literal.Text = html;

                WebControl control = (WebControl)controls[i];
                control.Attributes["onmouseover"] = string.Format("MoveObject(event.pageX,event.pageY,'{0}')", guid);
                control.Attributes["onmouseout"] = string.Format("HideObject('{0}')", guid);

                if (!control.Page.IsClientScriptBlockRegistered("tooltip.js"))
                {
                    control.Page.RegisterClientScriptBlock("tooltip.js", string.Format("<script language=javascript src='{0}'></script>", toolTipFilesFolder + "/" + jsFile));
                }

            }

        }

        public string ReadFileBody(string pathtofile)
        {
            string myline = string.Empty;
            using (StreamReader sr = new StreamReader(pathtofile))
            {
                String line;
                line = sr.ReadToEnd();
                myline = line;
            }
            return myline;

        }
    }
}