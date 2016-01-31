namespace GridShading.IntegrationTests
{
    using System;

    using GridShading.Solver;

    using NUnit.Framework;

    [TestFixture]
    public class EndToEndTests
    {
        [Test]
        public void Simple2X2Grid()
        {
            /*
             * 10
             * 01
             * 
             */
            var rows = new[] { new[] { 1 }, new[] { 1 } };
            var columns = new[] { new[] { 1 }, new[] { 1 } };

            var board = new[]
                            {
                                "1.",
                                ".."
                            };

            var solverOuter = new Solver();
            var result = solverOuter.Solve(new ProblemDefinition(rows, columns, board));

            Console.Write(string.Join(Environment.NewLine, result.GridPartialSolution.DrawSolution()));

            Assert.That(result.Solved, Is.True);
        }

        [Test]
        public void GchqProblem()
        {
            var rows = new[]
                           {
                               new [] { 7,3,1,1,7               }, 
                               new [] { 1,1,2,2,1,1             }, 
                               new [] { 1,3,1,3,1,1,3,1         },
                               new [] { 1,3,1,1,6,1,3,1         },
                               new [] { 1,3,1,5,2,1,3,1         },
                               new [] { 1,1,2,1,1               },
                               new [] { 7,1,1,1,1,1,7           },
                               new [] { 3,3                     },
                               new [] { 1,2,3,1,1,3,1,1,2       },
                               new [] { 1,1,3,2,1,1             },
                               new [] { 4,1,4,2,1,2             },
                               new [] { 1,1,1,1,1,4,1,3         },
                               new [] { 2,1,1,1,2,5             },
                               new [] { 3,2,2,6,3,1             },
                               new [] { 1,9,1,1,2,1             },
                               new [] { 2,1,2,2,3,1             },
                               new [] { 3,1,1,1,1,5,1           },
                               new [] { 1,2,2,5                 },
                               new [] { 7,1,2,1,1,1,3           },
                               new [] { 1,1,2,1,2,2,1           },
                               new [] { 1,3,1,4,5,1             },
                               new [] { 1,3,1,3,10,2            },
                               new [] { 1,3,1,1,6,6             },
                               new [] { 1,1,2,1,1,2             },
                               new [] { 7,2,1,2,5               }
                           };
            var columns = new[]
                            {
                               new [] { 7,2,1,1,7               }, 
                               new [] { 1,1,2,2,1,1             }, 
                               new [] { 1,3,1,3,1,3,1,3,1       },
                               new [] { 1,3,1,1,5,1,3,1         },
                               new [] { 1,3,1,1,4,1,3,1         },
                               new [] { 1,1,1,2,1,1             },
                               new [] { 7,1,1,1,1,1,7           },
                               new [] { 1,1,3                   },
                               new [] { 2,1,2,1,8,2,1           },
                               new [] { 2,2,1,2,1,1,1,2         },
                               new [] { 1,7,3,2,1               },
                               new [] { 1,2,3,1,1,1,1,1         },
                               new [] { 4,1,1,2,6               },
                               new [] { 3,3,1,1,1,3,1           },
                               new [] { 1,2,5,2,2               },
                               new [] { 2,2,1,1,1,1,1,2,1       },
                               new [] { 1,3,3,2,1,8,1           },
                               new [] { 6,2,1                   },
                               new [] { 7,1,4,1,1,3             },
                               new [] { 1,1,1,1,4               },
                               new [] { 1,3,1,3,7,1             },
                               new [] { 1,3,1,1,1,2,1,1,4       },
                               new [] { 1,3,1,4,3,3             },
                               new [] { 1,1,2,2,2,6,1           },
                               new [] { 7,1,3,2,1,1             }
                            };

            var board = new []
                            {
                                ".........................",
                                ".........................",
                                ".........................",
                                "...11.......11.......1...",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                "......11..1...11..1......",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                "......1....1....1...1....",
                                ".........................",
                                ".........................",
                                ".........................",
                                ".........................",
                                "...11....11....1....11...",
                                ".........................",
                                ".........................",
                                ".........................",
                            };

            var solverOuter = new Solver();
            var result = solverOuter.Solve(new ProblemDefinition(rows, columns, board));

            if (!result.Solved)
            {
                throw new Exception("Grid not solved");
            }

            Console.Write(string.Join(Environment.NewLine, result.GridPartialSolution.DrawSolution()));
            
            Assert.That(result.Solved, Is.True);
        }
    }
}
