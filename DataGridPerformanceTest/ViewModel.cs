using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using static DataGridPerformanceTest.BusinessDataObject;

namespace DataGridPerformanceTest
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ListCollectionView BusinessDataView { get; set; }


        public void UpdateViewModel(List<UsefulPoco> BusinessData)
        {
            BusinessDataView = new ListCollectionView(BusinessData);
            GroupBusinessData();
            //OnPropertyChanged("BusinessDataView");
        }

        private string _textToSearch;
        public void FilterBusinessDataView(string TextToSearch)
        {
            if (string.IsNullOrEmpty(TextToSearch))
                BusinessDataView.Filter = null;

            _textToSearch = TextToSearch;
            BusinessDataView.Filter = new Predicate<object>(FilterAction);
        }

        private bool FilterAction(object usefulPoco)
        {
            UsefulPoco poco = usefulPoco as UsefulPoco;
            return poco.Name.Contains(_textToSearch, StringComparison.InvariantCultureIgnoreCase);
        }

        private void GroupBusinessData()
        {
            BusinessDataView.GroupDescriptions.Clear();
            BusinessDataView.GroupDescriptions.Add(new PropertyGroupDescription("Classification1"));
        }
    }
}
