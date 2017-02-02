﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EMGraphics
{
    /// <summary>
    /// children in <see cref="ITreeNode&lt;T&gt;"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[Editor(typeof(IListEditor<ITreeNode<T>>), typeof(UITypeEditor))]
    public class ChildList<T> : IList<ITreeNode<T>>// where T : ITreeNode<T>
    {
        /// <summary>
        /// invoked when an item is added into this list.
        /// </summary>
        public event EventHandler<AddTreeNodeEventArgs<T>> ItemAdded;

        /// <summary>
        /// invoked when an item is removed from this list.
        /// </summary>
        public event EventHandler<RemoveTreeNodeEventArgs<T>> ItemRemoved;

        private List<ITreeNode<T>> list = new List<ITreeNode<T>>();

        /// <summary>
        /// parent of this list's items.
        /// </summary>
        private readonly ITreeNode<T> parent;

        /// <summary>
        /// children in <see cref="ITreeNode&lt;T&gt;"/>.
        /// </summary>
        /// <param name="parent"></param>
        public ChildList(ITreeNode<T> parent)
        {
            Debug.Assert(parent != null);

            this.parent = parent;
        }

        /// <summary>
        /// 搜索指定的对象，并返回整个 System.Collections.Generic.List&lt;T&gt; 中第一个匹配项的从零开始的索引。
        /// </summary>
        /// <param name="item">要在 System.Collections.Generic.List&lt;T&gt; 中定位的对象。对于引用类型，该值可以为 null。</param>
        /// <returns>如果在整个 System.Collections.Generic.List&lt;T&gt; 中找到 item 的第一个匹配项，则为该项的从零开始的索引；否则为-1。</returns>
        public int IndexOf(ITreeNode<T> item)
        {
            return list.IndexOf(item);
        }

        /// <summary>
        /// 将元素插入 System.Collections.Generic.List&lt;T&gt; 的指定索引处。
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 item。</param>
        /// <param name="item">要插入的对象。对于引用类型，该值可以为 null。</param>
        public void Insert(int index, ITreeNode<T> item)
        {
            item.Parent = this.parent;
            list.Insert(index, item);
            //item.RefreshRelativeTransform();

            EventHandler<AddTreeNodeEventArgs<T>> ItemAdded = this.ItemAdded;
            if (ItemAdded != null)
            { ItemAdded(this, new AddTreeNodeEventArgs<T>(item)); }
        }

        /// <summary>
        /// 移除 System.Collections.Generic.List&lt;T&gt; 的指定索引处的元素。
        /// </summary>
        /// <param name="index">要移除的元素的从零开始的索引。</param>
        public void RemoveAt(int index)
        {
            ITreeNode<T> obj = list[index];
            list.RemoveAt(index);
            obj.Parent = null;
            //obj.RefreshRelativeTransform();

            EventHandler<RemoveTreeNodeEventArgs<T>> ItemRemoved = this.ItemRemoved;
            if (ItemRemoved != null)
            { ItemRemoved(this, new RemoveTreeNodeEventArgs<T>(obj)); }
        }

        /// <summary>
        /// 获取或设置指定索引处的元素。
        /// </summary>
        /// <param name="index">要获得或设置的元素从零开始的索引。</param>
        /// <returns>指定索引处的元素。</returns>
        public ITreeNode<T> this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        /// <summary>
        /// 将对象添加到 System.Collections.Generic.List&lt;T&gt; 的结尾处。
        /// </summary>
        /// <param name="item">要添加到 System.Collections.Generic.List&lt;T&gt; 的末尾处的对象。对于引用类型，该值可以为 null。</param>
        public void Add(ITreeNode<T> item)
        {
            item.Parent = this.parent;
            list.Add(item);
            //item.RefreshRelativeTransform();

            EventHandler<AddTreeNodeEventArgs<T>> ItemAdded = this.ItemAdded;
            if (ItemAdded != null)
            { ItemAdded(this, new AddTreeNodeEventArgs<T>(item)); }
        }

        /// <summary>
        /// 将指定集合的元素添加到 System.Collections.Generic.List&lt;T&gt; 的末尾。
        /// </summary>
        /// <param name="items">一个集合，其元素应被添加到 System.Collections.Generic.List&lt;T&gt; 的末尾。集合自身不能为 null，但它可以包含为null 的元素（如果类型 T 为引用类型）。</param>
        public void AddRange(IEnumerable<ITreeNode<T>> items)
        {
            foreach (ITreeNode<T> item in items)
            {
                item.Parent = this.parent;
            }
            list.AddRange(items);
            foreach (ITreeNode<T> item in items)
            {
                //item.RefreshRelativeTransform();
            }

            EventHandler<AddTreeNodeEventArgs<T>> ItemAdded = this.ItemAdded;
            if (ItemAdded != null)
            {
                foreach (ITreeNode<T> item in items)
                {
                    ItemAdded(this, new AddTreeNodeEventArgs<T>(item));
                }
            }
        }

        /// <summary>
        /// 从 System.Collections.Generic.List&lt;T&gt;中移除所有元素。
        /// </summary>
        public void Clear()
        {
            ITreeNode<T>[] array = this.list.ToArray();
            this.list.Clear();

            foreach (ITreeNode<T> item in array)
            {
                item.Parent = null;
                //item.RefreshRelativeTransform();
            }

            EventHandler<RemoveTreeNodeEventArgs<T>> ItemRemoved = this.ItemRemoved;
            if (ItemRemoved != null)
            {
                foreach (ITreeNode<T> item in array)
                {
                    ItemRemoved(this, new RemoveTreeNodeEventArgs<T>(item));
                }
            }
        }

        /// <summary>
        /// 确定某元素是否在 System.Collections.Generic.List&lt;T&gt;中。
        /// </summary>
        /// <param name="item">要在 System.Collections.Generic.List&lt;T&gt; 中定位的对象。对于引用类型，该值可以为 null。</param>
        /// <returns>如果在 System.Collections.Generic.List&lt;T&gt; 中找到 item，则为 true，否则为 false。</returns>
        public bool Contains(ITreeNode<T> item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// 将整个 System.Collections.Generic.List&lt;T&gt; 复制到兼容的一维数组中，从目标数组的指定索引位置开始放置。
        /// </summary>
        /// <param name="array">作为从 System.Collections.Generic.List&lt;T&gt; 复制的元素的目标位置的一维 System.Array。System.Array必须具有从零开始的索引。</param>
        /// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制。</param>
        public void CopyTo(ITreeNode<T>[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取 System.Collections.Generic.List&lt;T&gt; 中实际包含的元素数。
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        /// <summary>
        /// 获取一个值，该值指示 System.Collections.Generic.ICollection&lt;T&gt; 是否为只读。
        /// </summary>
        public bool IsReadOnly
        {
            get { return ((ICollection<ITreeNode<T>>)(this.list)).IsReadOnly; }
        }

        /// <summary>
        /// 从 System.Collections.Generic.List&lt;T&gt; 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">要从 System.Collections.Generic.List&lt;T&gt; 中移除的对象。对于引用类型，该值可以为 null。</param>
        /// <returns>如果成功移除 item，则为 true；否则为 false。如果在 System.Collections.Generic.List&lt;T&gt; 中没有找到item，该方法也会返回 false。</returns>
        public bool Remove(ITreeNode<T> item)
        {
            bool result = list.Remove(item);
            if (result)
            {
                item.Parent = null;
                //item.RefreshRelativeTransform();

                EventHandler<RemoveTreeNodeEventArgs<T>> ItemRemoved = this.ItemRemoved;
                if (ItemRemoved != null)
                { ItemRemoved(this, new RemoveTreeNodeEventArgs<T>(item)); }
            }

            return result;
        }

        /// <summary>
        /// 返回循环访问 System.Collections.Generic.List&lt;T&gt; 的枚举数。
        /// </summary>
        /// <returns>用于 System.Collections.Generic.List&lt;T&gt; 的 System.Collections.Generic.List&lt;T&gt;.Enumerator。</returns>
        public IEnumerator<ITreeNode<T>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Count: {0}", this.list.Count);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AddTreeNodeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// newly added item.
        /// </summary>
        public ITreeNode<T> NewItem { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="newItem"></param>
        public AddTreeNodeEventArgs(ITreeNode<T> newItem)
        {
            this.NewItem = newItem;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Added item: {0}", NewItem);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RemoveTreeNodeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// removed item.
        /// </summary>
        public ITreeNode<T> RemovedItem { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="removedItem"></param>
        public RemoveTreeNodeEventArgs(ITreeNode<T> removedItem)
        {
            this.RemovedItem = removedItem;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Removed item: {0}", RemovedItem);
        }
    }
}