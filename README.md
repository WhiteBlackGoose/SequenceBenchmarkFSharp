# SequenceBenchmarkFSharp

## Code

The code does the following:
1. Filters elements `> 30`
1. Multiplies each element by 2
1. Filters elements `< 20000`
1. Multiplies each element by 3

Now, if it has suffix `with collect` it also does
1. Collects by emitting sequence { c + 1, c + 2, c + 3 } for each element c

## The ideas

We perform the listed operations on combinations:
1. List to list via list - that is, using `List.*` API
1. List to list via seq - that is, using `Seq.*` API and in the end doing `List.ofSeq`
1. Array to array via array - that is, using `Array.*` API
1. Array to array via seq - that is, using `Seq.*` API and in the end doing `Array.ofSeq`

## Benchmarks

|                                  Method |       Mean |     Error |     StdDev |     Median |   Gen 0 |   Gen 1 | Allocated |
|---------------------------------------- |-----------:|----------:|-----------:|-----------:|--------:|--------:|----------:|
|    'list to list via list with collect' | 111.739 us | 2.9493 us |  8.6497 us | 109.531 us | 91.4307 | 26.4893 |    312 KB |
|     'list to list via seq with collect' | 235.970 us | 5.9406 us | 17.1401 us | 233.715 us | 90.8203 | 30.2734 |    281 KB |
| 'array to array via array with collect' |  25.400 us | 0.5671 us |  1.6720 us |  25.067 us | 24.2615 |       - |     74 KB |
|   'array to array via seq with collect' | 227.939 us | 5.5082 us | 16.1546 us | 224.719 us | 75.4395 |       - |    232 KB |
|                 'list to list via list' |  33.528 us | 0.8047 us |  2.3345 us |  32.593 us | 40.6494 |       - |    125 KB |
|                  'list to list via seq' |  54.837 us | 1.6191 us |  4.6456 us |  54.638 us | 10.3149 |       - |     32 KB |
|              'array to array via array' |   5.945 us | 0.1165 us |  0.1708 us |   5.942 us |  5.2109 |       - |     16 KB |
|                'array to array via seq' |  59.242 us | 2.8116 us |  8.2015 us |  57.178 us |  4.0283 |       - |     13 KB |
