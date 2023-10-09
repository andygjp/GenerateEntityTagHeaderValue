```

BenchmarkDotNet v0.13.8, macOS Sonoma 14.0 (23A344) [Darwin 23.0.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD


```
| Method                   | Value                | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------- |--------------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| **UsingString**              | **1**                    | **117.58 ns** | **1.628 ns** | **1.523 ns** |  **1.00** |    **0.00** | **0.0038** |     **248 B** |        **1.00** |
| UsingSpans               | 1                    | 120.02 ns | 0.429 ns | 0.401 ns |  1.02 |    0.01 | 0.0041 |     256 B |        1.03 |
| UsingSpans_NoAssertions  | 1                    | 119.40 ns | 0.179 ns | 0.149 ns |  1.01 |    0.01 | 0.0041 |     256 B |        1.03 |
| UsingSpans_ToStringValue | 1                    | 107.99 ns | 0.215 ns | 0.191 ns |  0.92 |    0.01 | 0.0044 |     280 B |        1.13 |
| UsingSpans_GetCharCount  | 1                    |        NA |       NA |       NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 1                    | 128.57 ns | 0.107 ns | 0.100 ns |  1.09 |    0.01 | 0.0048 |     304 B |        1.23 |
| StraightToBase64         | 1                    |  56.36 ns | 0.068 ns | 0.060 ns |  0.48 |    0.01 | 0.0027 |     176 B |        0.71 |
|                          |                      |           |          |          |       |         |        |           |             |
| **UsingString**              | **100**                  | **121.98 ns** | **0.231 ns** | **0.216 ns** |  **1.00** |    **0.00** | **0.0038** |     **248 B** |        **1.00** |
| UsingSpans               | 100                  | 125.47 ns | 0.134 ns | 0.118 ns |  1.03 |    0.00 | 0.0041 |     256 B |        1.03 |
| UsingSpans_NoAssertions  | 100                  | 125.42 ns | 0.128 ns | 0.107 ns |  1.03 |    0.00 | 0.0038 |     256 B |        1.03 |
| UsingSpans_ToStringValue | 100                  | 114.55 ns | 0.180 ns | 0.150 ns |  0.94 |    0.00 | 0.0045 |     288 B |        1.16 |
| UsingSpans_GetCharCount  | 100                  |        NA |       NA |       NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 100                  | 132.53 ns | 0.210 ns | 0.175 ns |  1.09 |    0.00 | 0.0048 |     304 B |        1.23 |
| StraightToBase64         | 100                  |  56.89 ns | 0.143 ns | 0.127 ns |  0.47 |    0.00 | 0.0027 |     176 B |        0.71 |
|                          |                      |           |          |          |       |         |        |           |             |
| **UsingString**              | **18446744073709551615** | **228.60 ns** | **0.241 ns** | **0.188 ns** |  **1.00** |    **0.00** | **0.0062** |     **392 B** |        **1.00** |
| UsingSpans               | 18446744073709551615 | 235.16 ns | 0.842 ns | 0.787 ns |  1.03 |    0.00 | 0.0062 |     400 B |        1.02 |
| UsingSpans_NoAssertions  | 18446744073709551615 | 234.50 ns | 0.361 ns | 0.282 ns |  1.03 |    0.00 | 0.0062 |     400 B |        1.02 |
| UsingSpans_ToStringValue | 18446744073709551615 | 228.96 ns | 0.793 ns | 0.703 ns |  1.00 |    0.00 | 0.0074 |     464 B |        1.18 |
| UsingSpans_GetCharCount  | 18446744073709551615 |        NA |       NA |       NA |     ? |       ? |     NA |        NA |           ? |
| UsingCharArray           | 18446744073709551615 | 247.09 ns | 0.856 ns | 0.715 ns |  1.08 |    0.00 | 0.0076 |     488 B |        1.24 |
| StraightToBase64         | 18446744073709551615 |  56.81 ns | 0.078 ns | 0.061 ns |  0.25 |    0.00 | 0.0027 |     176 B |        0.45 |

Benchmarks with issues:
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=1]
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=100]
  CreateEntityTagHeaderValue.UsingSpans_GetCharCount: DefaultJob [Value=18446744073709551615]
