// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

int []tab = { 1, 2, 3, 4, 5, 6 };
Console.WriteLine(getAvg(tab));
Console.WriteLine(getMax(tab));
return;

static double getAvg(int[] arr)
{
    int sum = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        sum += arr[i];
    }

    return sum / arr.Length;
}
static int getMax(int[]arr)
{
	int max = 0;
	for	(int i = 0; i < arr.Length; i++)
	{
		if	(arr[i]>max)
		{
			max = arr[i];
		}
	}
	return max;
}