using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Tests
{
    public static class DummyData
    {
        public static ManualModel Manual1 => new ManualModel()
        {
            manualID = 1,
            name = "Test calculator",
            //status - active, development, inactive?
            status = "active",
            //number of trackpoints used in this manual
            trackPoints = 4,
            //List of scenarios
            scenarios = new List<ScenarioModel>()
            {
                new ScenarioModel()
                {
                    scenarioID = 1,
                    //Name of scenario
                    name = "Overview",
                    //Type - overview/step
                    type = "overview",
                    //Category(for prettier display)
                    category = "Basics",
                    order = 1,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 0
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 1
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 2
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 3
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 4
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 5
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 6
                        },
                    }
                },
                
                //placeholder
                new ScenarioModel()
                {
                    scenarioID = 10,
                    name = "How to calculate",
                    type = "step",
                    category = "Maintenance",
                    order = 11,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 200,
                            interactionPointID = 501,
                            order = 1
                        },
                        
                        new ScenarioInteractionModel()
                        {
                            interactionID = 201,
                            interactionPointID = 502,
                            order = 2
                        }
                    }
                },
                
                //placeholder
                new ScenarioModel()
                {
                    scenarioID = 11,
                    name = "Fix the button",
                    type = "step",
                    category = "Maintenance",
                    order = 12,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 202,
                            interactionPointID = 501,
                            order = 1
                        }
                    }
                }
            }
        };
        //-------------------------new test-------------------------
        public static ManualModel Manual2 => new ManualModel()
        {
            manualID = 2,
            name = "Prototype demonstration",
            status = "active",
            trackPoints = 1,
            scenarios = new List<ScenarioModel>()
            {
                new ScenarioModel()
                {
                    scenarioID = 100,
                    name = "Overview",
                    type = "overview",
                    category = "Overview",
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 100,
                            interactionPointID = 100
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 101,
                            interactionPointID = 101
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 102,
                            interactionPointID = 102
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 103,
                            interactionPointID = 103
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 104,
                            interactionPointID = 104
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 105,
                            interactionPointID = 105
                        },
                    }
                },
                new ScenarioModel()
                {
                    scenarioID = 101,
                    name = "DistanceCheck",
                    type = "overview",
                    category = "Testing",
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 106,
                            interactionPointID = 100
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 107,
                            interactionPointID = 103
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 108,
                            interactionPointID = 106
                        }
                    }
                },
                new ScenarioModel()
                {
                    scenarioID = 102,
                    name = "TestScenarioName",
                    type = "overview",
                    category = "Testing",
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 109,
                            interactionPointID = 100
                        },
                    }
                }
            }
        };

        public static List<InteractionPointModel> InteractionPoints = new List<InteractionPointModel>()
        {
            //Front panel
            new InteractionPointModel()
            {
                interactionPointID = 0,
                trackpointID = 0,
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                iPointName = "Front panel",
                content = "{\"text\":\"Open the panel\"}"
            },
            //Front panel backplate
            new InteractionPointModel()
            {
                interactionPointID = 1,
                trackpointID = 1,
                iPointName = "Front panel backplate",
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"Some setup info\"}"
            },
            //Numpad
            new InteractionPointModel()
            {
                interactionPointID = 2,
                trackpointID = 2,
                iPointName = "Numpad",
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"Find buttons\"}"
            },
            new InteractionPointModel()
            {
                interactionPointID = 3,
                trackpointID = 2,
                iPointName = "Numpad",
                // position = "{\"x\":1,\"y\":2,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"Equal btn\"}"
            },
            new InteractionPointModel()
            {
                interactionPointID = 4,
                trackpointID = 2,
                iPointName = "Numpad",
                // position = "{\"x\":-1,\"y\":-2,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"Equal btn(negative)\"}"
            },
            //Back
            //Info
            new InteractionPointModel()
            {
                interactionPointID = 5,
                trackpointID = 3,
                iPointName = "Numpad",
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"Some interesting text\"}"
            },
            //Change battery
            new InteractionPointModel()
            {
                interactionPointID = 6,
                trackpointID = 4,
                iPointName = "Numpad",
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                content = "{\"text\":\"TODO ABC\"}"
            },
            //-------------------------new test-------------------------
            new InteractionPointModel()
            {
                interactionPointID = 100,
                trackpointID = 0,
                iPointName = "M0_IP0",
                position = new Vector3(0, 0, 0),
                content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in tincidunt nisi. Donec mauris sapien, euismod congue erat eu, gravida hendrerit neque. Praesent a lorem ac velit pulvinar tincidunt id eu dui. Quisque id ullamcorper quam, eu tempus erat. Duis arcu ligula, volutpat sed mauris nec, venenatis dictum lacus. Morbi volutpat, metus eget auctor tincidunt, ipsum ex viverra lorem, a consequat sem nulla ut mi. Curabitur consectetur lobortis rutrum. Phasellus efficitur ligula eu semper blandit. Maecenas condimentum id dui nec tempor.\n\nUt condimentum felis augue, id tincidunt risus posuere auctor. Ut semper nunc at ante feugiat, ut efficitur est lacinia. Nulla facilisi. Sed rhoncus lobortis justo et pellentesque. In a hendrerit risus, non rhoncus nisi. Ut tempus tellus a ante vehicula scelerisque. Morbi consectetur augue quis massa suscipit consectetur. Donec ut elit vitae augue lacinia consequat. Phasellus id aliquam metus. Praesent at pulvinar dui. Nam vitae nunc quam. Vivamus nisl neque, vulputate eget tristique in, pellentesque ac est. Fusce ultrices ac eros vel aliquet. Aliquam erat volutpat. Aenean rhoncus ullamcorper nisi, ac aliquet lacus fermentum a. Morbi ornare, lacus at consectetur mollis, ipsum diam accumsan diam, gravida lobortis augue libero sed nisi."
            },
            new InteractionPointModel()
            {
                interactionPointID = 101,
                trackpointID = 1,
                iPointName = "M1_IP0+5y",
                position = new Vector3(0, 5, 0),
                content = "Suspendisse vel enim molestie, molestie magna vitae, pretium tellus. Etiam ornare nisi viverra nisi vehicula bibendum at fringilla purus. In a lectus efficitur, pharetra mi ac, consequat augue. Morbi mattis nunc ac nulla interdum porta. Sed convallis risus sed dolor viverra, in elementum justo vestibulum. Fusce a turpis nec arcu iaculis bibendum. Cras a dignissim nunc. Donec quis convallis risus, vitae accumsan ipsum. Vivamus et leo ut mi laoreet vehicula. Etiam vestibulum nulla ac enim hendrerit dictum. Integer egestas lorem ac imperdiet feugiat."
            },
            new InteractionPointModel()
            {
                interactionPointID = 102,
                trackpointID = 1,
                iPointName = "M1_IP1(TestLongName)-5y",
                position = new Vector3(0, -5, 0),
                content = "Fusce ornare consequat lorem at ultricies. Cras enim diam, mollis et finibus quis, accumsan vitae nisi. Ut interdum metus vel hendrerit auctor. Praesent quis felis aliquet, luctus quam id, malesuada libero. Proin nec semper metus. Mauris elementum vitae nibh vitae vehicula. Proin non ullamcorper sem. Sed sollicitudin fringilla tellus, non consectetur enim scelerisque vitae. Fusce commodo fringilla turpis at volutpat. Nam in malesuada lectus, quis aliquam odio. Nulla congue lectus at ipsum convallis, ac ultricies erat scelerisque. Duis pellentesque molestie arcu non iaculis. Vivamus semper, ex eu rhoncus finibus, augue justo facilisis sem, non ornare sem erat at nisi. Morbi sapien dui, elementum sit amet magna id, sagittis ornare massa. Integer aliquet turpis eu tempus mollis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.\n\nNulla facilisi. Curabitur id elementum sapien. Nullam et tellus justo. Nulla molestie mauris vel est commodo tincidunt. Curabitur id tincidunt nisl. Duis eget congue tellus. Nam facilisis augue ac dapibus gravida. Nullam sit amet dictum ipsum. Etiam iaculis, enim a ultricies ornare, metus diam feugiat tellus, ut efficitur augue dolor eget massa. Etiam blandit nunc diam, vitae scelerisque diam euismod et. Donec id faucibus orci. Proin vel fringilla mauris, eget ullamcorper arcu. Phasellus consequat dapibus est sit amet ultricies. Donec blandit non odio vitae euismod. Nam non arcu vel felis pharetra rutrum. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos."
            },
            new InteractionPointModel()
            {
                interactionPointID = 103,
                trackpointID = 1,
                iPointName = "M1_IP2+7y-5z",
                position = new Vector3(0, 7, -5),
                content = "Cras lacinia mauris massa, porta venenatis libero tincidunt eu. Aenean non varius nunc. Donec rutrum sapien vitae dolor pharetra congue. Pellentesque sit amet mi egestas ante tincidunt egestas sed vel nisl. Morbi vel sapien diam. Cras condimentum non augue sit amet dictum. Praesent neque ligula, tempor non turpis sit amet, efficitur pulvinar sem. Proin tempus metus eu rutrum finibus.\n\nNullam porta augue nec sem efficitur, commodo bibendum justo vehicula. Nulla nec tortor rutrum, posuere mi porttitor, cursus nunc. Nulla eget vestibulum odio, malesuada viverra ipsum. Vivamus metus neque, tincidunt sed sem a, condimentum laoreet nulla. Morbi porttitor ut quam nec porta. Morbi ac tellus mattis, consectetur odio eu, luctus tortor. Duis at ultricies libero, quis eleifend nisl. Duis viverra eget odio et lacinia. Duis mauris libero, finibus vel ornare ut, hendrerit eget risus. Praesent ac eleifend metus, a consequat nulla. Cras tincidunt a nulla non porttitor.\n\nSed rhoncus erat felis, quis venenatis tellus faucibus id. Ut iaculis purus eget urna laoreet, vel fringilla mi varius. Quisque posuere quis ipsum a sagittis. Sed malesuada libero erat, nec lobortis risus ornare vel. Curabitur pretium, diam non tempus congue, tellus tellus condimentum arcu, et consequat est lectus nec erat. Duis cursus nec felis porta consectetur. Curabitur viverra consectetur turpis, in porttitor urna semper hendrerit. Morbi scelerisque erat nec nibh venenatis mattis. Suspendisse potenti. Vivamus maximus diam a quam feugiat convallis. Nunc aliquam nibh mi, quis pulvinar risus sollicitudin et. Etiam fermentum, leo in viverra rhoncus, lectus mauris posuere sem, non cursus dolor ex vitae dolor. Integer et varius elit. Morbi vel odio ac nisi lobortis pulvinar. Donec bibendum sapien et enim vulputate, quis rutrum lectus vehicula. Cras euismod viverra leo."
            },
            new InteractionPointModel()
            {
                interactionPointID = 104,
                trackpointID = 2,
                iPointName = "M2_IP0+10x",
                position = new Vector3(10, 0, 0),
                content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in tincidunt nisi. Donec mauris sapien, euismod congue erat eu, gravida hendrerit neque. Praesent a lorem ac velit pulvinar tincidunt id eu dui. Quisque id ullamcorper quam, eu tempus erat. Duis arcu ligula, volutpat sed mauris nec, venenatis dictum lacus. Morbi volutpat, metus eget auctor tincidunt, ipsum ex viverra lorem, a consequat sem nulla ut mi. Curabitur consectetur lobortis rutrum. Phasellus efficitur ligula eu semper blandit. Maecenas condimentum id dui nec tempor.\n\nUt condimentum felis augue, id tincidunt risus posuere auctor. Ut semper nunc at ante feugiat, ut efficitur est lacinia. Nulla facilisi. Sed rhoncus lobortis justo et pellentesque. In a hendrerit risus, non rhoncus nisi. Ut tempus tellus a ante vehicula scelerisque. Morbi consectetur augue quis massa suscipit consectetur. Donec ut elit vitae augue lacinia consequat. Phasellus id aliquam metus. Praesent at pulvinar dui. Nam vitae nunc quam. Vivamus nisl neque, vulputate eget tristique in, pellentesque ac est. Fusce ultrices ac eros vel aliquet. Aliquam erat volutpat. Aenean rhoncus ullamcorper nisi, ac aliquet lacus fermentum a. Morbi ornare, lacus at consectetur mollis, ipsum diam accumsan diam, gravida lobortis augue libero sed nisi."
            },
            new InteractionPointModel()
            {
                interactionPointID = 105,
                trackpointID = 3,
                iPointName = "M3_IP0+10y",
                position = new Vector3(0, 10, 0),
                content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in tincidunt nisi. Donec mauris sapien, euismod congue erat eu, gravida hendrerit neque. Praesent a lorem ac velit pulvinar tincidunt id eu dui. Quisque id ullamcorper quam, eu tempus erat. Duis arcu ligula, volutpat sed mauris nec, venenatis dictum lacus. Morbi volutpat, metus eget auctor tincidunt, ipsum ex viverra lorem, a consequat sem nulla ut mi. Curabitur consectetur lobortis rutrum. Phasellus efficitur ligula eu semper blandit. Maecenas condimentum id dui nec tempor.\n\nUt condimentum felis augue, id tincidunt risus posuere auctor. Ut semper nunc at ante feugiat, ut efficitur est lacinia. Nulla facilisi. Sed rhoncus lobortis justo et pellentesque. In a hendrerit risus, non rhoncus nisi. Ut tempus tellus a ante vehicula scelerisque. Morbi consectetur augue quis massa suscipit consectetur. Donec ut elit vitae augue lacinia consequat. Phasellus id aliquam metus. Praesent at pulvinar dui. Nam vitae nunc quam. Vivamus nisl neque, vulputate eget tristique in, pellentesque ac est. Fusce ultrices ac eros vel aliquet. Aliquam erat volutpat. Aenean rhoncus ullamcorper nisi, ac aliquet lacus fermentum a. Morbi ornare, lacus at consectetur mollis, ipsum diam accumsan diam, gravida lobortis augue libero sed nisi."
            },
            new InteractionPointModel()
            {
                interactionPointID = 106,
                trackpointID = 1,
                iPointName = "M0_IP0+25x",
                position = new Vector3(25, 0, 0),
                content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in tincidunt nisi. Donec mauris sapien, euismod congue erat eu, gravida hendrerit neque. Praesent a lorem ac velit pulvinar tincidunt id eu dui. Quisque id ullamcorper quam, eu tempus erat. Duis arcu ligula, volutpat sed mauris nec, venenatis dictum lacus. Morbi volutpat, metus eget auctor tincidunt, ipsum ex viverra lorem, a consequat sem nulla ut mi. Curabitur consectetur lobortis rutrum. Phasellus efficitur ligula eu semper blandit. Maecenas condimentum id dui nec tempor.\n\nUt condimentum felis augue, id tincidunt risus posuere auctor. Ut semper nunc at ante feugiat, ut efficitur est lacinia. Nulla facilisi. Sed rhoncus lobortis justo et pellentesque. In a hendrerit risus, non rhoncus nisi. Ut tempus tellus a ante vehicula scelerisque. Morbi consectetur augue quis massa suscipit consectetur. Donec ut elit vitae augue lacinia consequat. Phasellus id aliquam metus. Praesent at pulvinar dui. Nam vitae nunc quam. Vivamus nisl neque, vulputate eget tristique in, pellentesque ac est. Fusce ultrices ac eros vel aliquet. Aliquam erat volutpat. Aenean rhoncus ullamcorper nisi, ac aliquet lacus fermentum a. Morbi ornare, lacus at consectetur mollis, ipsum diam accumsan diam, gravida lobortis augue libero sed nisi."
            },
        };

        public static List<TrackPointModel> TrackPoints = new List<TrackPointModel>()
        {
            new TrackPointModel()
            {
                trackpointID = 0,
                trackpointName = "Front panel",
                arucoID = 0,
                sizeCm = 6f,
            },
            new TrackPointModel()
            {
                trackpointID = 1,
                trackpointName = "Front panel backplate",
                arucoID = 1,
                sizeCm = 5f,
            },
            new TrackPointModel()
            {
                trackpointID = 2,
                trackpointName = "Numpad",
                arucoID = 2,
                sizeCm = 5f,
            },
            new TrackPointModel()
            {
                trackpointID = 3,
                trackpointName = "Calc back bottom",
                arucoID = 3,
                sizeCm = 4f,
            },
            new TrackPointModel()
            {
                trackpointID = 4,
                trackpointName = "Calc back top",
                arucoID = 4,
                sizeCm = 3f,
            }
        };
    }
}
