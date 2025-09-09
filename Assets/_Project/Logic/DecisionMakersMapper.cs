using System;
using System.Linq;

namespace _Project
{
    public class DecisionMakersMapper
    {
        private readonly IDecisionMaker[] _decisionMakers;
        private readonly DecisionMakersMap _map;

        public DecisionMakersMapper(IDecisionMaker[] decisionMakers, DecisionMakersMap map)
        {
            _decisionMakers = decisionMakers;
            _map = map;
        }

        public IDecisionMaker Get(int team)
        {
            Type type = _map.DecisionMakersPerType[team];
            return _decisionMakers.First(x => x.GetType() == type);
        }
    }
}