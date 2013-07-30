using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EquationEditor.Models.Elements;
using Microsoft.Practices.Prism.Commands;
using WpfUtils;

namespace EquationEditor.ViewModels.Elements
{
    public class ElementListVm : ViewModelBase
    {
        public ElementListVm(IElementRepository elementRepository)
        {
            _elements = elementRepository.Elements.ToList();
            LoadElementVms(elementRepository.Elements);
        }

        private readonly List<Element> _elements; 

        #region DoSearch Command

        private ICommand _doSearch;
        public ICommand DoSearch
        {
            get
            {
                return _doSearch ?? (_doSearch = new DelegateCommand<string>(DoTheSearch));
            }
        }

        void DoTheSearch(string searchText)
        {
            //System.Diagnostics.Debug.WriteLine("Began search");
            LoadElementVms
                (
                    string.IsNullOrEmpty(searchText)
                        ? _elements
                        : _elements.Where(
                            T =>
                            (T.ElementDescr.Contains(searchText)) || (T.ElementCode.Contains(searchText)))
                );
            //System.Diagnostics.Debug.WriteLine("Ended search\n\n");
        }

        #endregion // DoSearch Command
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        void LoadElementVms(IEnumerable<Element> elements)
        {
            ElementVms.Clear(); 
           foreach (var element in elements)
            {
                ElementVms.Add(new ElementVm(element));
            }

            //using (ObservableCollectionEx<ElementVm> iDelayed = ElementVms.DelayNotifications())
            //{
            //    foreach (var element in elements)
            //    {
            //        iDelayed.Add(new ElementVm(element));
            //    }
            //}
        }

        //ObservableCollectionEx<ElementVm> _elementVms = new ObservableCollectionEx<ElementVm>();
        //public ObservableCollectionEx<ElementVm> ElementVms
        //{
        //    get { return _elementVms; }
        //    set { _elementVms = value; }
        //}

        ObservableCollection<ElementVm> _elementVms = new ObservableCollection<ElementVm>();
        public ObservableCollection<ElementVm> ElementVms
        {
            get { return _elementVms; }
            set { _elementVms = value; }
        }

    }
}
