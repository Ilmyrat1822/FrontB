using System.Collections.ObjectModel;

namespace FrontB.Classes
{
    public class FilterDataContext
    {
        private ObservableCollection<string>[] filterItemsArray;
        private readonly int collectionCount;

        public FilterDataContext(int numberOfCollections = 11)
        {
            collectionCount = numberOfCollections;
            filterItemsArray = new ObservableCollection<string>[collectionCount];
        }
        public ObservableCollection<string> this[int index]
        {
            get
            {
                if (index < 0 || index >= collectionCount)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (filterItemsArray[index] == null)
                {
                    filterItemsArray[index] = new ObservableCollection<string>();
                }
                return filterItemsArray[index];
            }
        }

        public ObservableCollection<string> FilterItems => this[0];
        public ObservableCollection<string> FilterItems2 => this[1];
        public ObservableCollection<string> FilterItems3 => this[2];
        public ObservableCollection<string> FilterItems4 => this[3];
        public ObservableCollection<string> FilterItems5 => this[4];
        public ObservableCollection<string> FilterItems6 => collectionCount > 5 ? this[5] : null;
        public ObservableCollection<string> FilterItems7 => collectionCount > 6 ? this[6] : null;
        public ObservableCollection<string> FilterItems8 => collectionCount > 7 ? this[7] : null;
        public ObservableCollection<string> FilterItems9 => collectionCount > 8 ? this[8] : null;
        public ObservableCollection<string> FilterItems10 => collectionCount > 9 ? this[9] : null;
        public ObservableCollection<string> FilterItems11 => collectionCount > 10 ? this[10] : null;

        public void ClearAll()
        {
            for (int i = 0; i < collectionCount; i++)
            {
                if (filterItemsArray[i] != null)
                {
                    filterItemsArray[i].Clear();
                }
            }
        }

        public ObservableCollection<string> GetCollection(int index)
        {
            if (index >= 0 && index < collectionCount)
                return this[index];
            return null;
        }

        public ObservableCollection<string>[] GetAllCollections()
        {
            var collections = new ObservableCollection<string>[collectionCount];
            for (int i = 0; i < collectionCount; i++)
            {
                collections[i] = this[i];
            }
            return collections;
        }
    }
}
