namespace DoubleLinkedList
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }

        public T? Data { get; set; }
        public Node<T>? Previous { get; set; }
        public Node<T>? Next { get; set; }
    }


}