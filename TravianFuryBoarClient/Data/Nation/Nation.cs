using System;

namespace TravianFuryBoarClient.Data.Nation
{
    [Flags]
    public  enum Nation
    {
      German = 0,
      Gaul =   1 << 0,
      Romans = 1 << 1,
      Nature = 1 << 2,
      Natars = 1 << 3
    }
}
