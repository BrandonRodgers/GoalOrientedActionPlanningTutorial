using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Base
{
    public class Node
    {
        public Node parent;
        public float cost;
        public Dictionary<string, int> state;
        public GAction action;

        public Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
        {
            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(allStates);
            this.action = action;
        }
    }

    public class GPlanner
    {
        public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates states)
        {
            List<GAction> useableActions = new List<GAction>();
            foreach (GAction a in actions)
            {
                if (a.IsAchievable())
                {
                    useableActions.Add(a);
                }
            }
        
            List<Node> leaves = new List<Node>();
            Node startNode = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), null);

            bool success = BuildGraph(startNode, leaves, useableActions, goal);

            if (!success)
            {
                Debug.Log("NO PLAN");
                return null;
            }

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null)
                {
                    cheapest = leaf;
                }
                else if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }

            List<GAction> resultPlan = new List<GAction>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.action != null)
                {
                    resultPlan.Insert(0, n.action);
                }

                n = n.parent;
            }

            Queue<GAction> queue = new Queue<GAction>();
            foreach (GAction a in resultPlan)
            {
                queue.Enqueue(a);
            }
        
            Debug.Log("The Plan is: ");
            foreach (GAction a in queue)
            {
                Debug.Log($"Q: {a.actionName}");
            }

            return queue;
        }

        private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> useableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach (GAction action in useableActions)
            {
                if (action.IsAchievableGiven(parent.state))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                    foreach (KeyValuePair<string, int> effect in action.effects)
                    {
                        if (!currentState.ContainsKey(effect.Key))
                        {
                            currentState.Add(effect.Key, effect.Value);
                        }
                    }

                    Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                    if (GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<GAction> subset = ActionSubset(useableActions, action);
                        bool found = BuildGraph(node, leaves, subset, goal);
                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }

            return foundPath;
        }

        private bool GoalAchieved(Dictionary<string, int> goals, Dictionary<string, int> state)
        {
            foreach (KeyValuePair<string, int> goal in goals)
            {
                if (!state.ContainsKey(goal.Key))
                {
                    return false;
                }
            }

            return true;
        }

        private List<GAction> ActionSubset(List<GAction> actions, GAction removeAction)
        {
            List<GAction> subset = new List<GAction>();
            foreach (GAction a in actions)
            {
                if (!a.Equals(removeAction))
                {
                    subset.Add(a);
                }
            }

            return subset;
        }
    }
}