```

BenchmarkDotNet v0.13.8, macOS Sonoma 14.0 (23A344) [Darwin 23.0.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), Arm64 RyuJIT AdvSIMD


```
| Method         | Value                | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------- |--------------------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
| **UsingString**    | **W/&quot;dm(...)Jw==&quot; [32]** | **148.8 ns** | **0.30 ns** | **0.25 ns** |  **1.00** | **0.0057** |     **368 B** |        **1.00** |
| UsingSubString | W/&quot;dm(...)Jw==&quot; [32] | 134.5 ns | 0.10 ns | 0.08 ns |  0.90 | 0.0043 |     280 B |        0.76 |
| UsingCharArray | W/&quot;dm(...)Jw==&quot; [32] | 134.5 ns | 0.10 ns | 0.09 ns |  0.90 | 0.0043 |     280 B |        0.76 |
| UsingMemory    | W/&quot;dm(...)Jw==&quot; [32] | 137.2 ns | 0.12 ns | 0.09 ns |  0.92 | 0.0045 |     288 B |        0.78 |
|                |                      |          |         |         |       |        |           |             |
| **UsingString**    | **W/&quot;dmVyc2lvbicwJw==&quot;** | **116.5 ns** | **0.35 ns** | **0.31 ns** |  **1.00** | **0.0043** |     **272 B** |        **1.00** |
| UsingSubString | W/&quot;dmVyc2lvbicwJw==&quot; | 103.8 ns | 0.11 ns | 0.09 ns |  0.89 | 0.0032 |     208 B |        0.76 |
| UsingCharArray | W/&quot;dmVyc2lvbicwJw==&quot; | 103.5 ns | 0.10 ns | 0.09 ns |  0.89 | 0.0032 |     208 B |        0.76 |
| UsingMemory    | W/&quot;dmVyc2lvbicwJw==&quot; | 106.8 ns | 0.11 ns | 0.09 ns |  0.92 | 0.0033 |     216 B |        0.79 |
|                |                      |          |         |         |       |        |           |             |
| **UsingString**    | **W/&quot;dm(...)Jw==&quot; [24]** | **126.5 ns** | **1.05 ns** | **0.98 ns** |  **1.00** | **0.0045** |     **296 B** |        **1.00** |
| UsingSubString | W/&quot;dm(...)Jw==&quot; [24] | 114.4 ns | 0.50 ns | 0.44 ns |  0.90 | 0.0036 |     224 B |        0.76 |
| UsingCharArray | W/&quot;dm(...)Jw==&quot; [24] | 113.3 ns | 0.12 ns | 0.09 ns |  0.90 | 0.0036 |     224 B |        0.76 |
| UsingMemory    | W/&quot;dm(...)Jw==&quot; [24] | 115.8 ns | 0.09 ns | 0.07 ns |  0.91 | 0.0037 |     232 B |        0.78 |
