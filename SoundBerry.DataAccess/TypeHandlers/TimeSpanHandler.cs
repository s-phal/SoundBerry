using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SoundBerry.DataAccess.TypeHandlers
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            parameter.Value = (long)value.TotalSeconds;
        }

        public override TimeSpan Parse(object value)
        {
            return TimeSpan.FromSeconds((long)value);
        }
    }
}
