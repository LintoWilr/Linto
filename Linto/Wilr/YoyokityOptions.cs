using Linto.Wilr.DarkKnight;

namespace Linto.Wilr;

public class YoyokityOptions
{
	public static YoyokityOptions Instance = new YoyokityOptions();

	public void Reset()
	{
		Instance = new YoyokityOptions();
		Qt.Reset();
	}
}
