using System;
using System.Collections.Generic;
using System.Linq;

namespace MDPyramid
{
    class MDPyramid
    {
        static void Main(string[] args)
        {
            var content = @"215
                        192 124
                        117 269 442
                        218 836 347 235
                        320 805 522 417 345
                        229 601 728 835 133 124
                        248 202 277 433 207 263 257
                        359 464 504 528 516 716 871 182
                        461 441 426 656 863 560 380 171 923
                        381 348 573 533 448 632 387 176 975 449
                        223 711 445 645 245 543 931 532 937 541 444
                        330 131 333 928 376 733 017 778 839 168 197 197
                        131 171 522 137 217 224 291 413 528 520 227 229 928
                        223 626 034 683 839 052 627 310 713 999 629 817 410 121
                        924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

            //var content = @"1
            //                8 9
            //                1 5 9
            //                4 5 2 3";

            //var content = @"aaa";

            //var content = @"";

            GetPathGreatestSum(content);

        }

        public static void GetPathGreatestSum(string content)
        {
            var nodeList = ConvertDataTo2dList(content);
            if (nodeList == null)
            {
                Console.WriteLine("Input parsing failed.");
            }
            else
            {
                var nodesComplete = new List<List<int>>();
                var nodeChecksLeft = nodeList.Count > 1 ? true : false;
                var yHeight = nodeList.Count - 1;
                var maxSum = 0;
                while (nodeChecksLeft)
                {
                    var x = 0; // current column
                    var currentPathSum = nodeList[0][0];

                    // IF TOPMOST NODE ALREADY MARKED AS COMPLETE, FINISH THE LOOP
                    if (nodesComplete.Any(el => el[0] == 0 && el[1] == 0))
                    {
                        nodeChecksLeft = false;
                        break;
                    }
                    for (var y = 0; y < nodeList.Count - 1; y++)
                    {
                        var isEven = nodeList[y][x] % 2 == 0;

                        // IF BOTTOM NODE VALID, ADD TO PATH
                        if (!nodesComplete.Any(el => el[0] == y + 1 && el[1] == x) &&
                            ((isEven && nodeList[y + 1][x] % 2 != 0) || (!isEven && nodeList[y + 1][x] % 2 == 0)))
                        {
                            currentPathSum += nodeList[y + 1][x];
                            // If reached bottom row, conclude path and compare current path max sum
                            if (yHeight == y + 1)
                            {
                                maxSum = currentPathSum > maxSum ? currentPathSum : maxSum;
                                nodesComplete.Add(new List<int> { y + 1, x });
                                break;
                            }
                        }

                        // IF RIGHT-DIAGONAL NODE VALID, ADD TO PATH
                        else if (!nodesComplete.Any(el => el[0] == y + 1 && el[1] == x + 1) &&
                          ((isEven && nodeList[y + 1][x + 1] % 2 != 0) || (!isEven && nodeList[y + 1][x + 1] % 2 == 0)))
                        {
                            currentPathSum += nodeList[y + 1][x + 1];
                            x += 1; // move column cursor to the right
                                    // If reached bottom row, conclude path and compare current path max sum
                            if (yHeight == y + 1)
                            {
                                maxSum = currentPathSum > maxSum ? currentPathSum : maxSum;
                                nodesComplete.Add(new List<int> { y + 1, x });
                                break;
                            }
                        }
                        else
                        {
                            nodesComplete.Add(new List<int> { y, x });
                        }
                    }
                }
                Console.WriteLine("The greatest path sum is " + maxSum);
            }
        }

        public static List<List<int>> ConvertDataTo2dList(string content)
        {
            List<List<int>> outputList = new List<List<int>>();
            try
            {
                using (System.IO.StringReader reader = new System.IO.StringReader(content))
                {
                    string line = string.Empty;
                    do
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            var rowList = line.Trim().Split(' ');
                            var rowListParsed = new List<int>();
                            foreach (var stringNum in rowList)
                            {
                                rowListParsed.Add(Int32.Parse(stringNum));
                            }
                            outputList.Add(rowListParsed);
                        }

                    } while (line != null);
                }
                return outputList;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return null;
            }
        }
    }
}
