using System;
using System.Linq;
using Telerik.Pivot.Core;
using Telerik.Pivot.Core.Aggregates;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class PorcentajeSaldo : CalculatedItem
    {
        protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {
            double porc = 0;
            double emi = 0;
            double sal = 0;
            double baj = 0;
            try
            {
                emi = aggregateSummaryValues.GetAggregateValue("1.Emisiones").ConvertOrDefault<Double>();
            }
            catch { emi = 0; }

            try
            {
                sal = aggregateSummaryValues.GetAggregateValue("2.Cobros   ").ConvertOrDefault<Double>();
            }
            catch { sal = 0; }

            try
            {
                baj = aggregateSummaryValues.GetAggregateValue("3.Bajas    ").ConvertOrDefault<Double>();
            }
            catch { baj = 0; }

            porc = ((emi - (sal + baj)) / emi) * 100;

            return new DoubleAggregateValue(porc);
        }        
    }

    public class SaldoPendiente : CalculatedItem
    {
        protected override AggregateValue GetValue(IAggregateSummaryValues aggregateSummaryValues)
        {            
            double emi = 0;
            double cob = 0;
            double baj = 0;
            try
            {
                emi = aggregateSummaryValues.GetAggregateValue("1.Emisiones").ConvertOrDefault<Double>();
            }
            catch { emi = 0; }

            try
            {
                cob = aggregateSummaryValues.GetAggregateValue("2.Cobros   ").ConvertOrDefault<Double>();                
            }
            catch { cob = 0; }

            try
            {
                baj = aggregateSummaryValues.GetAggregateValue("3.Bajas    ").ConvertOrDefault<Double>();
            }
            catch { baj = 0; }
                      
            return new DoubleAggregateValue(emi - cob - baj);
        }
    }
}
