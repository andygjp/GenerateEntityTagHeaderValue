```

BenchmarkDotNet v0.13.8, macOS Sonoma 14.0 (23A344) [Darwin 23.0.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD


```
| Method                   | Value                | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------- |--------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| **UsingString**              | **1**                    | **116.9 ns** | **2.31 ns** | **2.37 ns** |  **1.00** |    **0.00** | **0.0038** |     **248 B** |        **1.00** |
| UsingSpans               | 1                    | 119.4 ns | 0.15 ns | 0.12 ns |  1.02 |    0.02 | 0.0041 |     256 B |        1.03 |
| UsingSpans_NoAssertions  | 1                    | 119.1 ns | 0.17 ns | 0.15 ns |  1.02 |    0.02 | 0.0041 |     256 B |        1.03 |
| UsingSpans_ToStringValue | 1                    | 107.7 ns | 0.27 ns | 0.23 ns |  0.92 |    0.02 | 0.0044 |     280 B |        1.13 |
| UsingSpans_GetCharCount  | 1                    |       NA |      NA |      NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 1                    | 126.7 ns | 0.14 ns | 0.13 ns |  1.08 |    0.02 | 0.0048 |     304 B |        1.23 |
|                          |                      |          |         |         |       |         |        |           |             |
| **UsingString**              | **100**                  | **121.6 ns** | **0.17 ns** | **0.16 ns** |  **1.00** |    **0.00** | **0.0038** |     **248 B** |        **1.00** |
| UsingSpans               | 100                  | 125.0 ns | 0.11 ns | 0.09 ns |  1.03 |    0.00 | 0.0041 |     256 B |        1.03 |
| UsingSpans_NoAssertions  | 100                  | 125.1 ns | 0.33 ns | 0.25 ns |  1.03 |    0.00 | 0.0041 |     256 B |        1.03 |
| UsingSpans_ToStringValue | 100                  | 113.7 ns | 0.26 ns | 0.24 ns |  0.94 |    0.00 | 0.0045 |     288 B |        1.16 |
| UsingSpans_GetCharCount  | 100                  |       NA |      NA |      NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 100                  | 131.8 ns | 0.20 ns | 0.17 ns |  1.08 |    0.00 | 0.0048 |     304 B |        1.23 |
|                          |                      |          |         |         |       |         |        |           |             |
| **UsingString**              | **18446744073709551615** | **228.5 ns** | **0.75 ns** | **0.70 ns** |  **1.00** |    **0.00** | **0.0062** |     **392 B** |        **1.00** |
| UsingSpans               | 18446744073709551615 | 233.3 ns | 0.32 ns | 0.28 ns |  1.02 |    0.00 | 0.0062 |     400 B |        1.02 |
| UsingSpans_NoAssertions  | 18446744073709551615 | 233.8 ns | 0.18 ns | 0.14 ns |  1.02 |    0.00 | 0.0062 |     400 B |        1.02 |
| UsingSpans_ToStringValue | 18446744073709551615 | 226.4 ns | 0.51 ns | 0.45 ns |  0.99 |    0.00 | 0.0074 |     464 B |        1.18 |
| UsingSpans_GetCharCount  | 18446744073709551615 |       NA |      NA |      NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 18446744073709551615 | 246.6 ns | 0.72 ns | 0.60 ns |  1.08 |    0.00 | 0.0076 |     488 B |        1.24 |

Benchmarks with issues:
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=1]
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=100]
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=18446744073709551615]
