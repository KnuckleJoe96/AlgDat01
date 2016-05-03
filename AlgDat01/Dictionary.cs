namespace AlgDat01
{
    public interface Dictionary
    {
        bool Search(int element); //true = gefunden
        bool Insert(int element); //true = hinzugefügt
        bool Delete(int element); //true = gelöscht
        void Print();          //Ausgabe der Elemente
    }

    public interface Set : Dictionary
    {

    }

    public interface SortedSet : Set
    {

    }

    public interface MultiSet : Dictionary
    {

    }

    public interface SortedMultiSet : MultiSet
    {

    }
}