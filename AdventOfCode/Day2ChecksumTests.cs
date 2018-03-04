﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace AdventOfCode
{
    public class Day2ChecksumTests
    {
        [Fact]
        public void Printer_checksum_test_returns_18()
        {
            // assemble
            var builder = new ChecksumBuilder();
            const int EXPECTED = 18;
            const string INPUT = @"
5 1 9 5
7 5 3
2 4 6 8
";
            // apply
            var checksum = builder.Checksum(INPUT);

            // assert
            Assert.Equal(EXPECTED, checksum);
        }

        [Fact]
        public void total_out_day2_kata()
        {
            // assemble
            var builder = new ChecksumBuilder();
            const string INPUT = @"
104	240	147	246	123	175	372	71	116	230	260	118	202	270	277	292
740	755	135	205	429	822	844	90	828	115	440	805	526	91	519	373
1630	991	1471	1294	52	1566	50	1508	1367	1489	55	547	342	512	323	51
1356	178	1705	119	1609	1409	245	292	1434	694	405	1692	247	193	1482	1407
2235	3321	3647	212	1402	3711	3641	1287	2725	692	1235	3100	123	144	104	101
1306	1224	1238	186	751	734	1204	1275	366	149	1114	166	1118	239	153	943
132	1547	1564	512	2643	2376	2324	2159	1658	107	1604	145	2407	131	2073	1878
1845	91	1662	108	92	1706	1815	1797	1728	1150	1576	83	97	547	1267	261
78	558	419	435	565	107	638	173	93	580	338	52	633	256	377	73
1143	3516	4205	3523	148	401	3996	3588	300	1117	2915	1649	135	134	182	267
156	2760	1816	2442	2985	990	2598	1273	167	821	138	141	2761	2399	1330	1276
3746	3979	2989	161	4554	156	3359	173	3319	192	3707	264	762	2672	4423	2924
3098	4309	4971	5439	131	171	5544	595	154	571	4399	4294	160	6201	4329	5244
728	249	1728	305	2407	239	691	2241	2545	1543	55	2303	1020	753	193	1638
260	352	190	877	118	77	1065	1105	1085	1032	71	87	851	56	1161	667
1763	464	182	1932	1209	640	545	931	1979	197	1774	174	2074	1800	939	161";

            // apply
            var checksum = builder.Checksum(INPUT);
        }

        [Fact]
        public void Given_input_should_return_total_9()
        {
            // assemble
            var builder = new ChecksumBuilder2();
            const int EXPECTED = 9;
            const string INPUT = @"
5 9 2 8
9 4 7 3
3 8 6 5";

            // apply
            var checksum = builder.Checksum(INPUT);

            // assert
            Assert.Equal(EXPECTED, checksum);
        }

        [Fact]
        public void Given_day2_test2_input_should_return_total_261()
        {
            // assemble
            var builder = new ChecksumBuilder2();
            const int EXPECTED = 261;
            const string INPUT = @"
104	240	147	246	123	175	372	71	116	230	260	118	202	270	277	292
740	755	135	205	429	822	844	90	828	115	440	805	526	91	519	373
1630	991	1471	1294	52	1566	50	1508	1367	1489	55	547	342	512	323	51
1356	178	1705	119	1609	1409	245	292	1434	694	405	1692	247	193	1482	1407
2235	3321	3647	212	1402	3711	3641	1287	2725	692	1235	3100	123	144	104	101
1306	1224	1238	186	751	734	1204	1275	366	149	1114	166	1118	239	153	943
132	1547	1564	512	2643	2376	2324	2159	1658	107	1604	145	2407	131	2073	1878
1845	91	1662	108	92	1706	1815	1797	1728	1150	1576	83	97	547	1267	261
78	558	419	435	565	107	638	173	93	580	338	52	633	256	377	73
1143	3516	4205	3523	148	401	3996	3588	300	1117	2915	1649	135	134	182	267
156	2760	1816	2442	2985	990	2598	1273	167	821	138	141	2761	2399	1330	1276
3746	3979	2989	161	4554	156	3359	173	3319	192	3707	264	762	2672	4423	2924
3098	4309	4971	5439	131	171	5544	595	154	571	4399	4294	160	6201	4329	5244
728	249	1728	305	2407	239	691	2241	2545	1543	55	2303	1020	753	193	1638
260	352	190	877	118	77	1065	1105	1085	1032	71	87	851	56	1161	667
1763	464	182	1932	1209	640	545	931	1979	197	1774	174	2074	1800	939	161";

            // apply
            var checksum = builder.Checksum(INPUT);

            // assert
            Assert.Equal(EXPECTED, checksum);
        }
    }

    public class ChecksumBuilder
    {
        private static readonly char[] SEPARATORS = new char[] { ' ', '\t' };

        public int Checksum(string printerCode)
        {
            using (var sr = new StringReader(printerCode))
            {
                string line;
                int checkSum = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    int[] numbers = line
                            .Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries)
                            .Select(a => int.Parse(a))
                            .ToArray();

                    var min = numbers.Min();
                    var max = numbers.Max();
                    checkSum += max - min;
                }

                return checkSum;
            }
        }
    }

    public class ChecksumBuilder2
    {
        private static readonly char[] SEPARATORS = new char[] { ' ', '\t' };

        public int Checksum(string printerCode)
        {
            using (var sr = new StringReader(printerCode))
            {
                string line;
                int checkSum = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    int[] numbers = line
                            .Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries)
                            .Select(a => int.Parse(a))
                            .OrderBy(a => a)
                            .ToArray();
                    for (int i = 0; i < numbers.Length - 1; i++)
                    {
                        for (int j = i + 1; j < numbers.Length; j++)
                        {
                            if (numbers[j] % numbers[i] == 0)
                            {
                                checkSum += (numbers[j] / numbers[i]);
                            }
                        }
                    }
                }

                return checkSum;
            }
        }
    }
}
