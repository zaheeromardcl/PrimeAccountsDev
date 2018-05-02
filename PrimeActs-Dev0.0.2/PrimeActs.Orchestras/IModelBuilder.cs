namespace PrimeActs.Orchestras
{
    public interface IModelBuilder<TViewModel, TDomainModel>
    {
        TViewModel CreateFrom(TDomainModel entity);
        TViewModel Rebuild(TViewModel model);
        TDomainModel ApplyChanges(TViewModel model);
    }
}