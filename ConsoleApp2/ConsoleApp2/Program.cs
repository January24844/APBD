// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

int []tab = { 1, 2, 3, 4, 5, 6 };
Console.WriteLine(getAvg(tab));

return;

static double getAvg(int[] arr)
{
    int summ = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        summ += arr[i];
    }

    return sum / arr.Length;
}