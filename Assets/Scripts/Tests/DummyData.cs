using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Tests
{
    public static class DummyData
    {
        public static List<ManualModel> Manuals = new List<ManualModel>()
        {
            Manual1,
            Manual2
        };
        public static ManualModel Template => new ManualModel()
        {
            manualID = -1,
            name = "Template",
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
                    name = "TemplateScenario",
                    //Type - overview/step
                    type = 0,
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
                    }
                },
            }
        };
        //-------------------------new test-------------------------
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
                    type = 0,
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
                    type = 1,
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
                    type = 1,
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
                    type = 0,
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
                    type = 0,
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
                    type = 0,
                    category = "Testing",
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 109,
                            interactionPointID = 100
                        },
                    }
                },
                new ScenarioModel()
                {
                    scenarioID = 103,
                    name = "Test step guide",
                    type = 1,
                    category = "Step guide",
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 110,
                            interactionPointID = 0,
                            order = 1
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 110,
                            interactionPointID = 50,
                            order = 2
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 111,
                            interactionPointID = 1,
                            order = 3
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 112,
                            interactionPointID = 2,
                            order = 4
                        },
                    }
                }
            }
        };
        //-------------------------new test-------------------------
        public static ManualModel CardboardTesting => new ManualModel()
        {
            manualID = 1,
            name = "CardboardTesting",
            //status - active, development, inactive?
            status = "active",
            //number of trackpoints used in this manual
            trackPoints = 5,
            //List of scenarios
            scenarios = new List<ScenarioModel>()
            {
                // Overview - 1 General Overview
                new ScenarioModel()
                {
                    scenarioID = 1,
                    //Name of scenario
                    name = "Overview",
                    //Type - overview - 0/step - 1
                    type = 0,
                    //Category(for prettier display)
                    category = "General Overview",
                    // Order in category
                    order = 1,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 0,
                            interactionPointID = 200,
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 1,
                            interactionPointID = 201
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 2,
                            interactionPointID = 202
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 3,
                            interactionPointID = 203
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 4,
                            interactionPointID = 204
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 5,
                            interactionPointID = 205
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 6,
                            interactionPointID = 206
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 7,
                            interactionPointID = 207
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 8,
                            interactionPointID = 208
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 9,
                            interactionPointID = 220
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 13,
                            interactionPointID = 213
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 14,
                            interactionPointID = 214
                        },
                    }
                },
                // Maintenance Hatch - 2 General Overview
                new ScenarioModel()
                {
                    scenarioID = 2,
                    name = "Maintenance Hatch",
                    type = 0,
                    category = "General Overview",
                    order = 2,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 15,
                            interactionPointID = 209
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 16,
                            interactionPointID = 211
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 17,
                            interactionPointID = 212
                        }
                    }
                },
                // Replace Filter - 3 Maintenance Step
                new ScenarioModel()
                {
                    scenarioID = 3,
                    name = "Replace Filter",
                    type = 1,
                    category = "Maintenance",
                    order = 1,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 18,
                            interactionPointID = 221,
                            order = 1
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 19,
                            interactionPointID = 222,
                            order = 2
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 20,
                            interactionPointID = 209,
                            order = 3,
                            overrideContent = "Open the latch to access the filter compartment"
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 21,
                            interactionPointID = 211,
                            order = 4,
                            overrideContent = "Remove the old filter and insert a new one."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 22,
                            interactionPointID = 212,
                            order = 5,
                            overrideContent = "If filter is installed, LED should be green."
                        },
                    }
                },
                // Device Startup Procedure - 4 Operation Step 
                new ScenarioModel()
                {
                    scenarioID = 4,
                    name = "Device Startup Procedure",
                    type = 1,
                    category = "Operation",
                    order = 1,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 23,
                            interactionPointID = 204,
                            order = 1,
                            overrideContent = "Ensure light is WHITE."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 24,
                            interactionPointID = 201,
                            order = 2,
                            overrideContent = "Press start button."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 25,
                            interactionPointID = 205,
                            order = 3,
                            overrideContent = "Display should display - Device is ready to use."
                        },
                    }
                },
                // Start program - 5 Operation Step 
                new ScenarioModel()
                {
                    scenarioID = 5,
                    name = "Start program",
                    type = 1,
                    category = "Operation",
                    order = 2,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 26,
                            interactionPointID = 208,
                            order = 1,
                            overrideContent = "Use numpad to enter program code, for avaible programs."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 27,
                            interactionPointID = 205,
                            order = 2,
                            overrideContent = "Confirm the entered code is correct, and program name is displayed."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 28,
                            interactionPointID = 206,
                            order = 3,
                            overrideContent = "Confirm the program."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 29,
                            interactionPointID = 204,
                            order = 4,
                            overrideContent = "Status indicator should be green, and display should display - Program is running.\n\n" +
                                              "If not, contact your technician.\n" +
                                              "Use Maintenance>Upload new program - to add new configurations"
                        },
                    }
                },
                // Replace Filter - 6 Maintenance Step
                new ScenarioModel()
                {
                    scenarioID = 6,
                    name = "Upload new program",
                    type = 1,
                    category = "Maintenance",
                    order = 2,
                    Interactions = new List<ScenarioInteractionModel>()
                    {
                        new ScenarioInteractionModel()
                        {
                            interactionID = 30,
                            interactionPointID = 208,
                            order = 1,
                            overrideContent = "Use numpad to enter maintenance code - 9999."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 32,
                            interactionPointID = 206,
                            order = 2,
                            overrideContent = "Confirm the maintenance procedure.\n\n" +
                                              "Do not disable power cable during the process."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 33,
                            interactionPointID = 214,
                            order = 3,
                            overrideContent = "Insert flash drive provided by manufacturer with program list."
                        },
                        new ScenarioInteractionModel()
                        {
                            interactionID = 31,
                            interactionPointID = 205,
                            order = 4,
                            overrideContent = "Choose desired program from the list to install."
                        },
                    }
                },
            }
        };
        
        //-------------------------INTERACTION POINTS-------------------------
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
            new InteractionPointModel()
            {
                interactionPointID = 50,
                trackpointID = 0,
                // position = "{\"x\":0,\"y\":0,\"z\":0}",
                position = new Vector3(0, 0, 0),
                iPointName = "Front panel-NextStep",
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
            //-------------------------cardboard-------------------------
            new InteractionPointModel()
            {
                interactionPointID = 200,
                trackpointID = 0,
                iPointName = "Model Info",
                position = new Vector3(0, 0, 0),
                content = "Model: VA‑305H\n" +
                          "Serial Number: FJ3LD‑FJEDL‑MOV‑9921\n" +
                          "Manufacturer: Cool Factory Name Ltd.\n\n" +
                          "Safety Notice:\n" +
                          "• Always disconnect the device from the power source before performing maintenance.\n" +
                          "• Do not operate the unit with the service cover removed.\n" +
                          "• Refer to the official documentation for detailed safety procedures."
            },
            //
            new InteractionPointModel()
            {
                interactionPointID = 201,
                trackpointID = 1,
                iPointName = "Start",
                position = new Vector3(0, 12, 0),
                content = "The Start control initiates the primary operation sequence of the device." +
                          "\nWhen activated, the system performs an internal readiness check and transitions into Active mode." +
                          "\nUse this control only when all safety conditions are met and the device is properly configured."
            },
            new InteractionPointModel()
            {
                interactionPointID = 202,
                trackpointID = 1,
                iPointName = "Stop",
                position = new Vector3(0, 9, 0),
                content = "The Stop control immediately halts all running operations." +
                          "\nThis action overrides any active process and forces the system into a safe idle state." +
                          "\nUse this control in emergency situations or when an operation must be terminated without delay."
            },
            new InteractionPointModel()
            {
                interactionPointID = 203,
                trackpointID = 1,
                iPointName = "Pause",
                position = new Vector3(0, 6, 0),
                content = "The Pause control temporarily suspends the current operation without resetting progress." +
                          "\nThis function is intended for short interruptions, allowing the user to resume the task from the same step." +
                          "\nIf the device remains paused for an extended period, it may automatically switch to standby mode."
            },
            new InteractionPointModel()
            {
                interactionPointID = 204,
                trackpointID = 1,
                iPointName = "Status indicator",
                position = new Vector3(4, 16, 0),
                content = "The status indicator displays the current operational state of the device." +
                          "\n\nPossible states include:" +
                          "\n• Ready (White) – the device is prepared for operation." +
                          "\n• Active (Green) – an operation is currently running." +
                          "\n• Warning (Yellow) – user attention is required." +
                          "\n• Error (Red) – the system has detected a fault." +
                          "\n\nUse this indicator to verify system readiness before starting any procedure."
            },
            //
            new InteractionPointModel()
            {
                interactionPointID = 205,
                trackpointID = 2,
                iPointName = "Display",
                position = new Vector3(0, 4, -3),
                content = "The main display provides real‑time feedback during operation and data entry. It shows digits, prompts and warnings as the user interacts with the device." +
                          "\nDuring diagnostics, the display may present multi‑step instructions or error identifiers, helping the user navigate complex procedures."
            },
            new InteractionPointModel()
            {
                interactionPointID = 206,
                trackpointID = 2,
                iPointName = "Confirm",
                position = new Vector3(6, 3, 0),
                content = "The Confirm control finalizes the current input and instructs the system to process it." +
                          "\nIf the value is valid, the device proceeds to the next step; otherwise, a corrective message appears on the display."
            },
            new InteractionPointModel()
            {
                interactionPointID = 207,
                trackpointID = 2,
                iPointName = "Cancel",
                position = new Vector3(6, -3, 0),
                content = "The Cancel control clears the current input or removes the last digit. It is used to correct mistakes or exit an input sequence when necessary." +
                          "\nUse this control whenever the displayed value does not match the intended input."
            },
            new InteractionPointModel()
            {
                interactionPointID = 208,
                trackpointID = 2,
                iPointName = "Numpad",
                position = new Vector3(15, 0, 0),
                content = "The numeric keypad is used to enter operational or diagnostic codes:" +
                          "\n• Each key press is shown on the display." +
                          "\n• Invalid combinations are rejected automatically." +
                          "\nThis keypad is used across multiple workflows, including configuration and maintenance."
            },
            //
            new InteractionPointModel()
            {
                interactionPointID = 220,
                trackpointID = 3,
                iPointName = "Maintenance hatch", //Main point
                position = new Vector3(-7, 2, 0),
                content = "The maintenance hatch provides access to the internal service area of the device." +
                          "\nIt is used for inspecting the filter, checking its condition and performing routine maintenance tasks." +
                          "\nOpening the hatch allows access to all service‑related components located on this module." +
                          "\n\nFor detailed instructions, refer to the scenario: Overview > Maintenance Hatch."
            },
            new InteractionPointModel()
            {
                interactionPointID = 209,
                trackpointID = 3,
                iPointName = "Cover latch",
                position = new Vector3(-6, 1.5f, 0),
                content = "The cover latch secures the service panel and must be released before accessing internal components; always disconnect power before operating the latch."
            },
            new InteractionPointModel()
            {
                interactionPointID = 221,
                trackpointID = 3,
                iPointName = "Latch screw 1",
                position = new Vector3(-6, 4, 0),
                content = "Unscrew the screw"
            },
            new InteractionPointModel()
            {
                interactionPointID = 222,
                trackpointID = 3,
                iPointName = "Latch screw 2",
                position = new Vector3(-6, -1.5f, 0),
                content = "Unscrew the screw"
            },
            new InteractionPointModel()
            {
                interactionPointID = 211,
                trackpointID = 3,
                iPointName = "Filter slot",
                position = new Vector3(-7.7f, 3.5f, 0),
                content = "The filter slot contains the replaceable airflow or dust filter used to maintain stable device performance." +
                          "\n• Remove the old filter during maintenance." +
                          "\n• Clean the slot before inserting a new one." +
                          "\n• Ensure correct orientation to avoid airflow issues."
            },
            new InteractionPointModel()
            {
                interactionPointID = 212,
                trackpointID = 3,
                iPointName = "Service LED",
                position = new Vector3(-7.7f, -1.3f, 0),
                content = "Service LED states:" +
                          "\n• OK (Green) – no action required." +
                          "\n• Warning (Yellow) – maintenance recommended." +
                          "\n• Replace (Red) – immediate service needed."
            },
            //
            new InteractionPointModel()
            {
                interactionPointID = 213,
                trackpointID = 4,
                iPointName = "Power port",
                position = new Vector3(-6, 0, 0),
                content = "The power port connects the device to the main power supply. Ensure the connector is fully seated and the cable is not damaged." +
                          "\nDisconnect power before opening the service panel."
            },
            new InteractionPointModel()
            {
                interactionPointID = 214,
                trackpointID = 4,
                iPointName = "Data port",
                position = new Vector3(-11, 0, 0),
                content = "The data port provides communication with external systems, diagnostic tools or update utilities. " +
                          "It supports firmware updates, configuration transfer and real‑time monitoring, and requires certified cables for stable operation."
            },
        };
        
        //-------------------------TRACK POINTS-------------------------
        public static List<TrackPointModel> TrackPoints = new List<TrackPointModel>()
        {
            // new TrackPointModel()
            // {
            //     trackpointID = 0,
            //     trackpointName = "Front panel",
            //     arucoID = 0,
            //     sizeCm = 6f,
            // },
            // new TrackPointModel()
            // {
            //     trackpointID = 1,
            //     trackpointName = "Front panel backplate",
            //     arucoID = 1,
            //     sizeCm = 5f,
            // },
            // new TrackPointModel()
            // {
            //     trackpointID = 2,
            //     trackpointName = "Numpad",
            //     arucoID = 2,
            //     sizeCm = 5f,
            // },
            // new TrackPointModel()
            // {
            //     trackpointID = 3,
            //     trackpointName = "Calc back bottom",
            //     arucoID = 3,
            //     sizeCm = 4f,
            // },
            // new TrackPointModel()
            // {
            //     trackpointID = 4,
            //     trackpointName = "Calc back top",
            //     arucoID = 4,
            //     sizeCm = 3f,
            // },
            //-------------------------cardboard-------------------------
            new TrackPointModel()
            {
                trackpointID = 0,
                trackpointName = "Info",
                arucoID = 0,
                sizeCm = 6f,
            },new TrackPointModel()
            {
                trackpointID = 1,
                trackpointName = "Buttons",
                arucoID = 1,
                sizeCm = 5f,
            },new TrackPointModel()
            {
                trackpointID = 2,
                trackpointName = "Numpad",
                arucoID = 2,
                sizeCm = 5f,
            },new TrackPointModel()
            {
                trackpointID = 3,
                trackpointName = "Service",
                arucoID = 3,
                sizeCm = 4f,
            },new TrackPointModel()
            {
                trackpointID = 4,
                trackpointName = "IOPanel",
                arucoID = 4,
                sizeCm = 3f,
            },
        };
    }
}
