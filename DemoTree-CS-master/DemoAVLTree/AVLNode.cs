using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAVLTree
{
    /// <summary>
    /// Nodo de un árbol AVL
    /// </summary>
    /// <typeparam name="E">Tipo de dato del elemento</typeparam>
    public class AVLNode<E>
    {
        /// <summary>
        /// Mantiene el elemento en el nodo
        /// </summary>
        private E item;
        public virtual E Item
        {
            get { return this.item; }
            set { this.item = value; }
        }
        /// <summary>
        /// Mantiene el enlace al subárbol izquierdo
        /// </summary>
        private AVLNode<E> left;
        public virtual AVLNode<E> Left
        {
            get { return this.left; }
            set { this.left = value; }
        }
        /// <summary>
        /// Mantiene el elace al subárbol derecho
        /// </summary>
        private AVLNode<E> right;
        public virtual AVLNode<E> Right
        {
            get { return this.right; }
            set { this.right = value; }
        }
        /// <summary>
        /// Mantiene el factor de equilibrio del nodo
        /// </summary>
        private int balance;
        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }


        /// <summary>
        /// Constructor especializado, permite fijar el elemento del nodo
        /// y el valor de los enlaces a subárboles izquierdo y derecho
        /// </summary>
        /// <param name="item">Elemento en el nodo</param>
        /// <param name="next">Enlace al subárbol izquierdo</param>
        /// <param name="prev">Enlace al subárbol derecho</param>
        public AVLNode(E item = default(E), AVLNode<E> left = null, AVLNode<E> right = null)
        {
            this.Item = item;
            this.Left = left;
            this.Right = right;
        }

        /// <summary>
        /// Metodo para probar las distintas formas en que
        /// se puede recorrer un árbol
        /// </summary>
        public virtual void Visit()
        {
            Console.Write("{0} ", this.Item.ToString());
        }
    }
}
