/* modified original from www.dofactory.com 
 DoFactory.BusinessLayer.BusinessObjects */

using System.Collections.Generic;

namespace Nairc.KPWPortal
{
    /// <summary>
    /// ProxyList represents a generic list that is loaded from the database. 
    /// It implements a pattern called lazy loading (also called just-in-time loading).  
    /// Lazy loading is a way to control how and when a list is loaded with the goal 
    /// to load it only when it's absolutely necessary. This way the class prevent the 
    /// application from executing unneccessary database hits. It's a way to better 
    /// manage limited resources.
    /// 
    /// ProxyList is used in the Customer business object to hold a reference to the list of Orders. 
    /// See ProxyForOrderDetails in the Facade layer.
    /// 
    /// ProxyList is also used in the Order business objec to hold a reference to the list of Order Details. 
    /// See ProxyForOrders in the Facade layer.
    /// 
    /// GoF Design Patterns: Proxy.
    /// Enterprise Design Patterns: Lazyload.
    /// </summary>
    /// <remarks>
    /// This class is a Proxy because it 'stands in' for a limited resource.
    /// This class is a Lazy-loader because it does not load when constructed.
    /// </remarks>
    /// <typeparam name="T">Generic type stored in the list.</typeparam>
    public abstract class ProxyList<T> : IList<T>
    {
        private object _parent;
        private IList<T> _list;
        private bool _isLoaded = false;

        /// <summary>
        /// Default constructor for ProxyList abstract class.
        /// </summary>
        public ProxyList()
        {
        }

        /// <summary>
        /// Overloaded constructor for ProxyList abstract class.
        /// </summary>
        /// <param name="parent">Parent (or owner) of the ProxyList.</param>
        public ProxyList(object parent)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Overloaded constructor for ProxyList abstract class.
        /// </summary>
        /// <param name="parent">Parent (or owner) of the ProxyList.</param>
        /// <param name="list">List being proxied.</param>
        public ProxyList(object parent, IList<T> list)
        {
            this.Parent = parent;
            this.List = list;
        }

        /// <summary>
        /// Loads the specific list. To be implemented in derived class.
        /// </summary>
        /// <returns></returns>
        protected abstract int LoadList();

        /// <summary>
        /// Lazy-loads the list. First checks if the list is already loaded.
        /// </summary>
        private void LazyLoad()
        {
            if (!_isLoaded)
                LoadList();
        }

        /// <summary>
        /// Gets or sets the types list or types being proxied.
        /// </summary>
        public IList<T> List
        {
            get { return _list; }
            set
            {
                _list = value;
                _isLoaded = true;
            }
        }

        /// <summary>
        /// Gets or sets the parent class (i.e. the owner) or the ProxyList.
        /// </summary>
        public object Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Determines the index of a specific item in the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is already loaded.
        /// If not, it first lazy-loads the list.
        /// </remarks>
        /// <param name="item">Item for which index is requested.</param>
        /// <returns>Index of the item in the list.</returns>
        public int IndexOf(T item)
        {
            LazyLoad();
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item at the given index in the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="index">Index at which item is inserted.</param>
        /// <param name="item">Item being inserted.</param>
        public void Insert(int index, T item)
        {
            LazyLoad();
            _list.Insert(index, item);
        }

        /// <summary>
        /// Removes the list item at the specified index.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="index">Index position.</param>
        public void RemoveAt(int index)
        {
            LazyLoad();
            _list.RemoveAt(index);
        }

        /// <summary>
        /// Indexer into list. Get or set item at given index.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="index">Index at which item is located.</param>
        /// <returns>Type at index location.</returns>
        public T this[int index]
        {
            get
            {
                LazyLoad();
                return _list[index];
            }
            set
            {
                LazyLoad();
                _list[index] = value;
            }
        }

        #region ICollection<T> Members

        /// <summary>
        /// Adds an item to the list.
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            LazyLoad();
            _list.Add(item);
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            if (_isLoaded)
                _list.Clear();
        }

        /// <summary>
        /// Determines whether the list contains a specific item.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="item">Item being looked for.</param>
        /// <returns>Value indicating whether item is present in list.</returns>
        public bool Contains(T item)
        {
            LazyLoad();
            return _list.Contains(item);
        }

        /// <summary>
        /// Copies the element of list into an array starting at a given index.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="array">Generic array being copied into.</param>
        /// <param name="arrayIndex">Start index from which to copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            LazyLoad();
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements in the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        public int Count
        {
            get
            {
                LazyLoad();
                return _list.Count;
            }
        }

        /// <summary>
        /// Gets the value indicating whether the list is read-only.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        public bool IsReadOnly
        {
            get
            {
                LazyLoad();
                return _list.IsReadOnly;
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific item in the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <param name="item">Item being searched for.</param>
        /// <returns>Value indicating whether item was removed.</returns>
        public bool Remove(T item)
        {
            LazyLoad();
            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns a generic enumerator that iterates over the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <returns>Requested generic enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            LazyLoad();
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Returns an enumerator that iterates over the list.
        /// </summary>
        /// <remarks>
        /// Before the operation starts it checks whether list is loaded.
        /// If not, it first lazy-loads it.
        /// </remarks>
        /// <returns>Requested enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            LazyLoad();
            return _list.GetEnumerator();
        }

        #endregion
    }
}