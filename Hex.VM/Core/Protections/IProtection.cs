namespace Hex.VM.Core.Protections
{
	public abstract class IProtection
	{
		public abstract void Execute(Context context);
	}
}
