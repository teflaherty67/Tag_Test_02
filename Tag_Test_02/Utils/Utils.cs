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

        internal static bool ElementIsTagged(Document curDoc, View viewName, Element elem, BuiltInCategory bicTag, BuiltInCategory bicElements)
        {
            // collect the tags
            List<Element> m_colTags = new FilteredElementCollector(curDoc, viewName.Id)
                .OfCategory(bicTag)
                .Where(x => x is IndependentTag)
                .ToList();

            // collect the elements
            List<Element> m_colElems = new FilteredElementCollector(curDoc, viewName.Id)
                .OfCategory(bicElements)
                .ToList();

            // extract the IDs of the tagged elements
            List<ElementId> elemIDs = new List<ElementId>();

            foreach (Element curElem in m_colTags)
            {
                IndependentTag curTag = curElem as IndependentTag;

                ElementId elemId = curTag.TaggedLocalElementId;

                int aux1 = curTag.TaggedLocalElementId.IntegerValue;

                bool aux2 = elemIds.Contains(curTag.TaggedLocalElementId);

                if ((tag.TaggedLocalElementId.IntegerValue != -1)
                    && (ids.Contains(tag.TaggedLocalElementId) == false))
                {
                    ids.Add(tag.TaggedLocalElementId);
                }
            }

            // Transforma a una lista de Unique Items
            List<ElementId> uniqueIds = ids.Distinct().ToList();

            // Se extraen los ids de los elementos
            List<ElementId> eleIds = new List<ElementId>();

            foreach (Element e in eleColl)
            {
                eleIds.Add(e.Id);
            }

            // Se compara si los elementos estan taggeados
            List<ElementId> idEleTag = new List<ElementId>();

            foreach (ElementId id in eleIds)
            {
                if (uniqueIds.Contains(id))
                {
                    idEleTag.Add(id);
                }
            }

            // COMPARACION - Si el elemento esta en la lista de elementos taggeados, entrega "true"
            // de lo contrario, entrega "false".

            if (idEleTag.Contains(ele.Id))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static FamilySymbol GetTagByName(Document curDoc, string tagName)
        {
            // get all loaded door & window tags
            List<BuiltInCategory> m_colTags = new List<BuiltInCategory>();
            m_colTags.Add(BuiltInCategory.OST_DoorTags);
            m_colTags.Add(BuiltInCategory.OST_WindowTags);

            ElementMulticategoryFilter filter = new ElementMulticategoryFilter(m_colTags);

            IList<Element> tElements = new FilteredElementCollector(curDoc)
                .WherePasses(filter)
                .WhereElementIsElementType()
                .ToElements();

            foreach (Element elem in tElements)
            {
                if (elem.get_Parameter(BuiltInParameter.SYMBOL_FAMILY_NAME_PARAM).AsString() == tagName)
                    return elem as FamilySymbol;
            }

            return null;
        }

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

            // collect all the tags in the view
            List<Element> m_colTags = new FilteredElementCollector(curDoc, viewName.Id)
               .OfCategory(BuiltInCategory.OST_Tags)
               .Where(x => x is IndependentTag)
               .ToList();

            // collect all the elements in the view
            List<Element> m_colElements = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.Ele)
                .ToList();

            // set the tag family
            FamilySymbol tagDoor = Utils.GetTagByName(curDoc, "Door Tag-Type Comments");
            FamilySymbol tagWndw = Utils.GetTagByName(curDoc, "Window Tag-Type Name and Head Height");

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
