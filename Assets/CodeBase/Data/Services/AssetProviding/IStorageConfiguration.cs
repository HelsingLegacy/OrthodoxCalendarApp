namespace CodeBase.Data.Services.AssetProviding
{
  public interface IStorageConfiguration
  {
    void BindDataPath();
    void CreateDataFolders();
  }
}