using CodeBase.UI.ContentHandlers.NonInteracting;

namespace CodeBase.Infrastructure.Services.Factory
{
  public interface IProgressFactory
  {
    ProgressBar CreateProgress(int days);
  }
}