namespace LollyShared
{
    public interface MWordInterface
    {
        int LANGID { get; set; }
        string WORD { get; set; }
        string NOTE { get; set; }
        int FAMIID { get; set; }
        int LEVEL { get; set; }
    }
    public interface MPhraseInterface
    {
        int LANGID { get; set; }
        string PHRASE { get; set; }
        string TRANSLATION { get; set; }
    }
}
