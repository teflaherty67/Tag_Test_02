using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tag_Test_02
{
    internal static class Utils
    {
        #region Ribbon
        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel currentPanel = GetRibbonPanelByName(app, tabName, panelName);

            if (currentPanel == null)
                currentPanel = app.CreateRibbonPanel(tabName, panelName);

            return currentPanel;
        }

        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            return null;
        }

        #endregion

        #region Tags

        internal static void TagAllUntaggedDoorsWindowsInView(Document curDoc, View viewName, string tagName)
        {
            // create multicategory list
            List<BuiltInCategory> m_listCats = new List<BuiltInCategory>();
            m_listCats.Add(BuiltInCategory.OST_Windows);
            m_listCats.Add(BuiltInCategory.OST_Doors);

            // create mutlicategory filter
            ElementMulticategoryFilter filter = new ElementMulticategoryFilter(m_listCats);

            // get all doors & windows in the current view
            IList<Element> m_catElements = new FilteredElementCollector(curDoc, viewName.Id)
                .WherePasses(filter)
                .WhereElementIsNotElementType()
                .ToElements();

            // filter out already tagged doors & windows - How??

            // set the tag family - How??

            // set the tag parameters
            TagMode tMode = TagMode.TM_ADDBY_CATEGORY;
            TagOrientation tOrient = TagOrientation.AnyModelDirection;

            // loop through the untagged elements

            // create a reference for the element

            // get the element location

            // tag the element


            throw new NotImplementedException();
        }

        internal static void MarkAllDoorsWindowsInView(Document curDoc, View viewName, string tagName)
        {

            // create multicategory list
            List<BuiltInCategory> m_listCats = new List<BuiltInCategory>();
            m_listCats.Add(BuiltInCategory.OST_Windows);
            m_listCats.Add(BuiltInCategory.OST_Doors);

            // create mutlicategory filter
            ElementMulticategoryFilter filter = new ElementMulticategoryFilter(m_listCats);
            throw new NotImplementedException();
        }

        #endregion

        #region Views

        internal static List<View> GetAllViews(Document curDoc)
        {
            {
                FilteredElementCollector m_colviews = new FilteredElementCollector(curDoc);
                m_colviews.OfCategory(BuiltInCategory.OST_Views);

                List<View> m_views = new List<View>();
                foreach (View x in m_colviews.ToElements())
                {
                    if (x.IsTemplate == false)

                        m_views.Add(x);
                }

                return m_views;
            }
        }

        internal static List<View> GetAllViewsByNameContains(Document curDoc, string viewName)
        {
            List<View> m_Views = Utils.GetAllViews(curDoc);

            List<View> m_ViewsToTag = new List<View>();

            foreach (View curView in m_Views)
            {
                if (curView.Name.Contains(viewName))
                    m_ViewsToTag.Add(curView);
            }

            return m_ViewsToTag;
        }       

        #endregion
    }
}
