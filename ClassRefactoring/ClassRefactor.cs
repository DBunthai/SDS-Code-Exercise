using System;
using System.Collections.Generic;

namespace DeveloperSample.ClassRefactoring
{

    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }


    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }

    public class Swallow
    {
        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public double GetAirspeedVelocity()
        {
            if (AirspeedVelocityMap.Use().TryGetValue((Type, Load), out var airspeedVelocity))
            {
                return airspeedVelocity.GetAirspeedVelocity();
            }
            throw new InvalidOperationException();
        }


    }

    public class AirspeedVelocityMap
    {
        public static Dictionary<(SwallowType, SwallowLoad), AireSpeedSwallow> Use()
        {
            return new()
            {
                { (SwallowType.African, SwallowLoad.None), new AfricanNoSwallow() },
                { (SwallowType.African, SwallowLoad.Coconut), new AfricanSwallow() },
                { (SwallowType.European, SwallowLoad.None), new EuropeanNoSwallow() },
                { (SwallowType.European, SwallowLoad.Coconut), new EuropeanSwallow() }
            };
        }

        public interface AireSpeedSwallow
        {
            double GetAirspeedVelocity();

        }

        public class AfricanNoSwallow : AireSpeedSwallow
        {
            public double GetAirspeedVelocity()
            {
                return 22;
            }
        }

        public class AfricanSwallow : AireSpeedSwallow
        {
            public double GetAirspeedVelocity()
            {
                return 18;
            }
        }

        public class EuropeanNoSwallow : AireSpeedSwallow
        {
            public double GetAirspeedVelocity()
            {
                return 20;
            }
        }

        public class EuropeanSwallow : AireSpeedSwallow
        {
            public double GetAirspeedVelocity()
            {
                return 16;
            }
        }

    }
}