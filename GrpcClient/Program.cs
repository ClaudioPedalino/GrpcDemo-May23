using GrpcClient.Helpers;


Console.CursorVisible = false;
int selectedIndex = 0;

while (true)
{
    ConsoleKeyInfo keyInfo = ConsoleHelper.GetMenuOptionSelected(selectedIndex, Consts.Options);


    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        selectedIndex = ConsoleHelper.HandleIsUpKey(selectedIndex, Consts.Options);
    }
    else if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        selectedIndex = ConsoleHelper.HandleIsDownKey(selectedIndex, Consts.Options);
    }
    else if (keyInfo.Key == ConsoleKey.Enter)
    {
        selectedIndex = await ConsoleHelper.HandleOption(selectedIndex, Consts.Options);
    }
    Console.Clear();
}