namespace TwitterAnalysis.Infrastructure.Data.Query
{
    public static class TrainingQuery
    {
        public static string TrainingRacistPhrasesQuery => "select * from \"RacistComment\" ";

        public static string TrainingInsertRacistPhraseQuery => $"insert into \"RacistComment\" (\"Text\", \"ActiveRacist\") values (@Text, @ActiveRacist) RETURNING * ";
    }
}
