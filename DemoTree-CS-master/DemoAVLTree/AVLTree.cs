using System;
using System.Text;

namespace DemoAVLTree
{
    /// <summary>
    /// Implementación de árbol AVL
    /// </summary>
    /// <typeparam name="E">Tipo de dato de elementos que se introduce en el árbol</typeparam>
    public class AVLTree<E> where E : IComparable
    {

        /// <summary>
        /// Mantiene la raíz del árbol
        /// </summary>
        private AVLNode<E> root;
        protected virtual AVLNode<E> Root
        {
            get { return this.root; }
            set { this.root = value; }
        }

        /// <summary>
        /// Indica si el árbol está vacio
        /// </summary>
        public virtual Boolean IsEmpty
        {
            get { return this.Root == null; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AVLTree()
        {
            this.Root = null;
        }

        /// <summary>
        /// Constructor especializado, permite asignar el valor del item de la raíz del árbol y 
        /// los subárboles izquierdo y derecho
        /// </summary>
        /// <param name="root">referencia al item de la raíz del árbol, puede ser null</param>
        /// <param name="left">referencia a un árbol que estará a la izquierda, puede ser null</param>
        /// <param name="right">referencia a un árbol que estará a la derecha, puede ser null</param>
        public AVLTree(E item = default(E), AVLTree<E> leftTree = null, AVLTree<E> rightTree = null)
        {
            this.Root = new AVLNode<E>(item);
            if (leftTree != null)
            {
                this.Root.Left = leftTree.Root;
            }
            if (rightTree != null)
            {
                this.Root.Right = rightTree.Root;
            }
        }

        /// <summary>
        /// Representación parentizada del árbol
        /// </summary>
        /// <returns>Cadena con la representación</returns>
        public override string ToString()
        {
            return ToString(this.Root);
        }
        /// <summary>
        /// Implementación recursiva de la representación parentizada del árbol
        /// </summary>
        /// <param name="root">Nodo del árbol</param>
        /// <returns>Cadena con la representación</returns>
        protected virtual string ToString(AVLNode<E> root)
        {
            StringBuilder sb = new StringBuilder();
            if (root != null)
            {
                sb.Append(root.Item.ToString());
                if (root.Left != null)
                {
                    sb.Append(" (" + ToString(root.Left));
                    if (root.Right != null)
                    {
                        sb.Append(", " + ToString(root.Right));
                    }
                    sb.Append(")");
                }
                else
                {
                    if (root.Right != null)
                    {
                        sb.Append(" (, " + ToString(root.Right) + ")");
                    }
                }
            }
            return sb.ToString(); ;
        }

        /// <summary>
        /// Implementación del recorrido Pre Orden
        /// </summary>
        public virtual void PreOrder()
        {
            PreOrder(this.Root);
        }
        /// <summary>
        /// Implementación recursiva de Pre Orden
        /// </summary>
        /// <param name="root">Nodo a visitar</param>
        protected virtual void PreOrder(AVLNode<E> root)
        {
            if (root != null)
            {
                root.Visit();
                PreOrder(root.Left);
                PreOrder(root.Right);
            }
        }
        /// <summary>
        /// Implementación del recorrido En Orden
        /// </summary>
        public virtual void InOrder()
        {
            InOrder(this.Root);
        }
        /// <summary>
        /// Implementación recursiva de En Orden
        /// </summary>
        /// <param name="root">Nodo a visitar</param>
        protected virtual void InOrder(AVLNode<E> root)
        {
            if (root != null)
            {
                InOrder(root.Left);
                root.Visit();
                InOrder(root.Right);
            }
        }
        /// <summary>
        /// Implementación del recorrido Post Orden
        /// </summary>
        public virtual void PostOrder()
        {
            PostOrder(this.Root);
        }
        /// <summary>
        /// Implementación recursiva de PostOrden
        /// </summary>
        /// <param name="root">Nodo a visitar</param>
        protected virtual void PostOrder(AVLNode<E> root)
        {
            if (root != null)
            {
                PostOrder(root.Left);
                PostOrder(root.Right);
                root.Visit();
            }
        }

        /// <summary>
        /// Determina si un elemento está contenido en el árbol
        /// </summary>
        /// <param name="item">Elemento a controlar</param>
        /// <returns>Verdadero si el elemento se encuentra contenido en el árbol</returns>
        public virtual Boolean Contains(E item)
        {
            return Contains(this.Root, item);
        }
        /// <summary>
        /// Implementación recursivoa para determinar si un elemento está contenido en el árbol
        /// </summary>
        /// <param name="root">Nodo del subárbol a considerar</param>
        /// <param name="item">Elemento a controlar</param>
        /// <returns>Verdadero si el elemento se encuentra contenido en el árbol</returns>
        protected virtual Boolean Contains(AVLNode<E> root, E item)
        {
            if (root == null)
            {
                return false;
            }
            if (item.CompareTo(root.Item) < 0)
            {
                return Contains(root.Left, item);
            }
            else
            {
                if (item.CompareTo(root.Item) > 0)
                {
                    return Contains(root.Right, item);
                }
            }
            return true;
        }
        /// <summary>
        /// Busca un elemento en el árbol
        /// </summary>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Referencia al elemento o null si no está en el árbol</returns>
        public virtual E Find(E item)
        {
            return Find(this.Root, item);
        }
        /// <summary>
        /// Implementación recursiva que busca un elemento en el árbol
        /// </summary>
        /// <param name="root">Nodo subárbol a considerar</param>
        /// <param name="item">Elemento a buscar</param>
        /// <returns>Referencia al elemento o null si no está en el árbol</returns>
        protected virtual E Find(AVLNode<E> root, E item)
        {
            if (root == null)
            {
                return default(E);
            }
            if (item.CompareTo(root.Item) < 0)
            {
                return Find(root.Left, item);
            }
            else
            {
                if (item.CompareTo(root.Item) > 0)
                {
                    return Find(root.Right, item);
                }
            }
            return root.Item;
        }

        /// <summary>
        /// Agrega un elemento en el árbol AVL
        /// </summary>
        /// <param name="item">Elemento a agregar</param>
        public virtual void Add(E item)
        {
            bool flag = false;
            this.Root = AddAVL(this.Root, item, ref flag);
        }
        /// <summary>
        /// Implementación recursiva que agrega un elemento en un árbol AVL
        /// </summary>
        /// <param name="root">Nodo del subárbol a considerar</param>
        /// <param name="item">Elemento a agregar</param>
        /// <param name="flag">Indica si cambió la altura</param>
        /// <returns>Nodo del subárbol considerado</returns>
        protected virtual AVLNode<E> AddAVL(AVLNode<E> root, E item, ref bool flag)
        {
            AVLNode<E> n1;
            // Si el árbol está vacio, simplement se agrega 
            // y se indica que cambió la altura
            if (root == null)
            {
                root = new AVLNode<E>(item);
                flag = true;
            }
            else
            {
                // El valor del elemento a agregar es menor
                // que el valor de la raíz del subárbol a considerar
                if (item.CompareTo(root.Item) < 0)
                {
                    // Se agrega recursivamente por la izquierda
                    root.Left = AddAVL(root.Left, item, ref flag);
                    if (flag) // si cambió la altura ?
                    {
                        switch (root.Balance)
                        {
                            case -1:
                                // antes izquierda < derecha, después se equilibra
                                // y cambia la bandera
                                root.Balance = 0;
                                flag = false;
                                break;
                            case 0:
                                // antes izquierda = derecha, después izquierda mas grande
                                // en algún nivel superior puede hacer falta un rebalanceo
                                // la bandera queda como estaba (true)
                                root.Balance = 1;
                                break;
                            case 1:
                                // antes izquierda > derecha, hay que rebalancear
                                n1 = root.Left;
                                if (n1.Balance == 1)
                                {
                                    // la izquierda de la izquierda es mayor
                                    // rotación simple izquierda-izquierda
                                    root = LeftLeftRotation(root, n1);
                                }
                                else
                                {
                                    // rotación doble izquierda-derecha
                                    root = LeftRightRotation(root, n1);
                                }
                                // subárbol ajustado cambia la bandera
                                flag = false;
                                break;
                        }
                    }
                }
                else
                {
                    // El valor del elemento a agregar es mayor
                    // que el valor de la raíz del subárbol a considerar
                    if (item.CompareTo(root.Item) > 0)
                    {
                        // Se agrega recursivamente por la derecha
                        root.Right = AddAVL(root.Right, item, ref flag);
                        if (flag) // si cambió la altura ?
                        {
                            switch (root.Balance)
                            {
                                case -1:
                                    // antes izquierda < derecha, hay que rebalancer
                                    n1 = root.Right;
                                    if (n1.Balance == -1)
                                    {
                                        // la derecha de la derecha es mayor
                                        // rotación simple derecha-derecha
                                        root = RightRightRotation(root, n1);
                                    }
                                    else
                                    {
                                        // rotación doble derecha-izquierda
                                        root = RightLeftRoation(root, n1);
                                    }
                                    // subárbol ajustado cambia la bandera
                                    flag = false;
                                    break;
                                case 0:
                                    // antes izquierda = derecha, después la derecha mas grande
                                    // en algún nivel superior puede hacer falta un rebalanceo
                                    // la bandera queda como estaba (true)
                                    root.Balance = -1;
                                    break;
                                case 1:
                                    // antes izquierda > derecha, despues se equilibra
                                    // y cambia la bandera
                                    root.Balance = 0;
                                    flag = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // No se puede almacenar claves repetidas
                        throw new Exception("Claves repetidas");
                    }
                }
            }
            return root;
        }

        /// <summary>
        /// Extrae un elemento del árbol AVL
        /// </summary>
        /// <param name="item">Elemento a extraer</param>
        public virtual void Remove(E item)
        {
            bool flag = false;
            this.Root = RemoveAVL(this.Root, item, ref flag);
        }
        /// <summary>
        /// Implementación recursiva que extraer un elemento del árbol AVL
        /// </summary>
        /// <param name="root">Nodo del subárbol a considerar</param>
        /// <param name="item">Elemento a extraer</param>
        /// <param name="flag">Indica que cambió la altura</param>
        /// <returns>Nodo raiz del subárbol considerado</returns>
        protected virtual AVLNode<E> RemoveAVL(AVLNode<E> root, E item, ref bool flag)
        {
            // No se puede extraer nodos de un árbol vacío
            if (root == null)
            {
                throw new Exception("No existe");
            }
            // El valor del elemento a extraer es menor
            // que el valor de la raíz del subárbol a considerar
            if (item.CompareTo(root.Item) < 0)
            {
                // Se procesa recursivamente por la izquierda
                root.Left = RemoveAVL(root.Left, item, ref flag);
                if (flag)
                {
                    // Cambió la altura, analizar el balance
                    root = LeftBalance(root, ref flag);
                }
            }
            else
            {
                // El valor del elemento a extraer es mayor 
                // que el valor de la raíz del subárbol a considerar
                if (item.CompareTo(root.Item) > 0)
                {
                    // Se procesa recursivamente por la derecha
                    root.Right = RemoveAVL(root.Right, item, ref flag);
                    if (flag)
                    {
                        // Cambió la altura, analizar el balance
                        root = RightBalance(root, ref flag);
                    }
                }
                else
                { // encontrado
                    AVLNode<E> q;
                    q = root;
                    if (q.Left == null)
                    {
                        // Un único descendiente, cambia la altura
                        root = q.Right;
                        flag = true;
                    }
                    else
                    {
                        if (q.Right == null)
                        {
                            // Un único descendiente, cambia la altura
                            root = q.Left;
                            flag = true;
                        }
                        else
                        { // tiene dos subárboles !!!
                            root.Left = Repleace(root, root.Left, ref flag);
                            if (flag)
                            {
                                // Cambió la altura, analizar el balance
                                root = LeftBalance(root, ref flag);
                            }
                            q = null;
                        }
                    }
                }
            }
            return root;
        }

        /// <summary>
        /// Aplica mayor de los menores controlando el cambio de altura
        /// La técnica aplicada es eliminación por copia con búsqueda recursiva
        /// </summary>
        /// <param name="n">Nodo que debe extraerse</param>
        /// <param name="act">Nodo que corresponde al mayor de los menores</param>
        /// <param name="flag">Bandera que indica que cambió la altura</param>
        /// <returns></returns>
        protected virtual AVLNode<E> Repleace(AVLNode<E> n, AVLNode<E> act, ref bool flag)
        {
            if (act.Right != null)
            {
                // hay algo a la derecha, es mayor que el actual
                act.Right = Repleace(n, act.Right, ref flag);
                if (flag)
                {
                    // Cambió la altura, analizar el balance
                    act = RightBalance(act, ref flag);
                }
            }
            else
            {
                // act es el mayor de los menores
                // copiar el elemento y anular la referencia
                // cambia la altura
                n.Item = act.Item;
                n = act;
                act = act.Left;
                n = null;
                flag = true;
            }
            return act;
        }
        /// <summary>
        /// Analiza las posibles rotaciones cuando se extrajo un nodo de la izquierda
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="flag">)Indica que cambió la altura</param>
        /// <returns>Nodo considerado</returns>
        protected virtual AVLNode<E> LeftBalance(AVLNode<E> n, ref bool flag)
        {
            AVLNode<E> n1;
            switch (n.Balance)
            {
                case 1:
                    // antes derecha < izquierda, después se equilibra
                    n.Balance = 0;
                    break;
                case 0:
                    // antes derecha = izquierda, despues se ajusta y cambia la altura
                    n.Balance = -1;
                    flag = false;
                    break;
                case -1:
                    // antes derecha > izquierda, hay que rebalancer
                    n1 = n.Right;
                    if (n1.Balance <= 0)
                    {
                        flag = false;
                        n = RightRightRotation(n, n1);
                    }
                    else
                    {
                        n = RightLeftRoation(n, n1);
                    }
                    break;
            }
            return n;
        }
        /// <summary>
        /// Analiza las posibles rotaciones cuando se extrajo un nodo de la derecha 
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="flag">Indica que cambió la altura</param>
        /// <returns>Nodo considerado</returns>
        protected virtual AVLNode<E> RightBalance(AVLNode<E> n, ref bool flag)
        {
            AVLNode<E> n1;
            switch (n.Balance)
            {
                case 1:
                    // antes derecha < izquierda, hay que rebalancer
                    n1 = n.Left;
                    if (n1.Balance >= 0)
                    {
                        if (n1.Balance == 0)
                        {
                            flag = false;
                        }
                        n = LeftLeftRotation(n, n1);
                    }
                    else
                    {
                        n = LeftRightRotation(n, n1);
                    }
                    break;
                case 0:
                    // antes derecha = izquierda, después se ajusta y cambia la altura
                    n.Balance = 1;
                    flag = false;
                    break;
                case -1:
                    // antes derecha > izquierda, después se equilibra
                    n.Balance = 0;
                    break;
            }
            return n;
        }

        /// <summary>
        /// Rotacion simple Izquierda - Izquierda
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="n1">Nodo a la izquierda del que se considera</param>
        /// <returns>Nuevo nodo raíz</returns>
        protected virtual AVLNode<E> LeftLeftRotation(AVLNode<E> n, AVLNode<E> n1)
        {
            // Agrego esta sentencia para mostrar que se realiza una rotación Left-Left
            Console.Write(" LL ");
            n.Left = n1.Right;
            n1.Right = n;
            if (n1.Balance == 1)
            {
                n.Balance = 0;
                n1.Balance = 0;
            }
            else
            {
                n.Balance = 1; // CUIDADO !!!
                n1.Balance = -1;
            }
            return n1;
        }
        /// <summary>
        /// Rotacion doble Izquierda - Derecha
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="n1">Nodo a la izquierda del que se considera</param>
        /// <returns>Nuevo nodo raíz</returns>
        protected virtual AVLNode<E> LeftRightRotation(AVLNode<E> n, AVLNode<E> n1)
        {
            // Agrego esta sentencia para mostrar que se realiza una rotación Left-Right
            Console.Write(" LR ");
            // Nodo a la derecha del nodo que está a la izquierda del nodo a considerar
            AVLNode<E> n2;
            n2 = n1.Right;
            n.Left = n2.Right;
            n2.Right = n;
            n1.Right = n2.Left;
            n2.Left = n1;
            n1.Balance = (n2.Balance == -1) ? 1 : 0; // CUIDADO !!!
            n.Balance = (n2.Balance == 1) ? -1 : 0;
            n2.Balance = 0;
            return n2;
        }
        /// <summary>
        /// Rotacion simple Derecha - Derecha
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="n1">Nodo a la derecha del que se considera</param>
        /// <returns>Nuevo nodo raíz</returns>
        protected virtual AVLNode<E> RightRightRotation(AVLNode<E> n, AVLNode<E> n1)
        {
            // Agrego esta sentencia para mostrar que se realiza una rotación Right-Right
            Console.Write(" RR ");
            n.Right = n1.Left;
            n1.Left = n;
            if (n1.Balance == -1)
            {
                n.Balance = 0;
                n1.Balance = 0;
            }
            else
            {
                n.Balance = -1; // CUIDADO !!!
                n1.Balance = 1;
            }
            return n1;
        }
        /// <summary>
        /// Rotacion doble Derecha - Izquierda
        /// </summary>
        /// <param name="n">Nodo a considerar</param>
        /// <param name="n1">Nodo a la derecha del que se considera</param>
        /// <returns>Nuevo nodo raíz</returns>
        protected virtual AVLNode<E> RightLeftRoation(AVLNode<E> n, AVLNode<E> n1)
        {
            // Agrego esta sentencia para mostrar que se realiza una rotación Right-Left
            Console.Write(" RL ");
            // Nodo a la izquierda del nodo que está a la derecha del nodo a considerar
            AVLNode<E> n2;
            n2 = n1.Left;
            n.Right = n2.Left;
            n2.Left = n;
            n1.Left = n2.Right;
            n2.Right = n1;
            n.Balance = (n2.Balance == -1) ? 1 : 0; // CUIDADO !!!
            n1.Balance = (n2.Balance == 1) ? -1 : 0;
            n2.Balance = 0;
            return n2;
        }
        
    }
}
