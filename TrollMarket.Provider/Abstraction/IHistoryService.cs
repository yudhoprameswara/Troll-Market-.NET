using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DTO.History;

namespace TrollMarket.Provider.Abstraction
{
    public interface IHistoryService
    {
        public HistoryIndexDTO GetIndex(int page,string buyerId, string seller);

    }
}
