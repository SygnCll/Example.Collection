using Example.Collection.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Collection.Domain
{
    public partial class Dealer
    {
        public Dealer(long id)
        {
            Id = id;
        }

        public Dealer(Dealer dealer, Institution institution, Region region, City city, Town town, DealerType dealerType) : this()
        {
            Institution = institution;
            DealerCode = dealer.DealerCode;
            DealerType = dealerType;
            Status = dealer.Status;
            Address = dealer.Address;
            PostalCode = dealer.PostalCode;
            Region = region;
            City = city;
            Town = town;
            Longitude = dealer.Longitude;
            Latitude = dealer.Latitude;

            if (dealer.DealerEmailAddresses != null)
            {
                foreach (var dealerEmailAddressDTO in dealer.DealerEmailAddresses)
                {
                    DealerEmailAddresses.Add(new DealerEmailAddress(this, dealerEmailAddressDTO.IsDefault, dealerEmailAddressDTO.EmailAddress));
                }
            }

            if (dealer.DealerPhoneNumbers != null)
            {
                foreach (var dealerPhoneNumberDTO in dealer.DealerPhoneNumbers)
                {
                    DealerPhoneNumbers.Add(new DealerPhoneNumber(this, dealerPhoneNumberDTO.IsDefault, dealerPhoneNumberDTO.PhoneNumber));
                }
            }

            dealer.DealerLocalization
                  .ToList()
                  .ForEach(dealerLocalization => DealerLocalization.Add(dealerLocalization.Key, new DealerLocalization(dealerLocalization.Value.ToString())));
        }

        public virtual ICollection<Dealer> GetAllSubDealers()
        {
            ICollection<Dealer> dealers = new List<Dealer>();
            ExpandSubDealersRecursively(this, dealers);
            dealers.Remove(this);
            return dealers;
        }


        public virtual void SetDealerCode(string dealerCode)
        {
            DealerCode = dealerCode;
        }

        public virtual void SetDealerType(DealerType dealerType)
        {
            DealerType = dealerType;
        }

        public virtual void SetLocation(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public virtual void SetProxyOwner(Dealer proxyOwnerDealer)
        {
            ProxyOwnerDealer = proxyOwnerDealer;

        }

        protected virtual void SetStatus(StatusTypeEnum status)
        {
            Status = status;
        }



        public virtual void Update(Dealer dealer, DealerType dealerType, Region region, City city, Town town, bool updateStatus)
        {
            Address = dealer.Address;
            PostalCode = dealer.PostalCode;
            Region = region;
            City = city;
            Town = town;
            DealerType = dealerType;
            Longitude = dealer.Longitude;
            Latitude = dealer.Latitude;

            dealer.DealerLocalization
                  .ToList()
                  .ForEach(dealerLocalization =>
                  {
                      if (DealerLocalization.ContainsKey(dealerLocalization.Key))
                      {
                          DealerLocalization[dealerLocalization.Key].Update(dealerLocalization.Value.ToString());
                      }
                      else
                      {
                          DealerLocalization.Add(dealerLocalization.Key, new DealerLocalization(dealerLocalization.Value.ToString()));
                      }
                  });

            if (dealer.DealerEmailAddresses != null)
            {
                foreach (var dealerEmailAddress in dealer.DealerEmailAddresses)
                {
                    DealerEmailAddresses.Add(new DealerEmailAddress(this, dealerEmailAddress.IsDefault, dealerEmailAddress.EmailAddress));
                }

                DealerEmailAddresses.ToList()
                                    .ForEach(dealerEmailAddress =>
                                    {
                                        if (!dealer.DealerEmailAddresses.Select(x => x.EmailAddress).Contains(dealerEmailAddress.EmailAddress))
                                        {
                                            DealerEmailAddresses.Remove(dealerEmailAddress);
                                        }
                                    });
            }

            if (dealer.DealerPhoneNumbers != null)
            {
                foreach (var dealerPhoneNumber in dealer.DealerPhoneNumbers)
                {
                    DealerPhoneNumbers.Add(new DealerPhoneNumber(this, dealerPhoneNumber.IsDefault, dealerPhoneNumber.PhoneNumber));
                }

                DealerPhoneNumbers.ToList()
                                  .ForEach(dealerPhoneNumber =>
                                  {
                                      if (!dealer.DealerPhoneNumbers.Select(x => x.PhoneNumber).Contains(dealerPhoneNumber.PhoneNumber))
                                      {
                                          DealerPhoneNumbers.Remove(dealerPhoneNumber);
                                      }
                                  });
            }

            if (updateStatus)
            {
                SetStatus(dealer.Status);
            }
        }

        public virtual void UpdateParentDealer(Dealer parentDealer, DealerHierarchy dealerHierarchy)
        {
            ParentDealer = parentDealer;
            ParentDealerCode = parentDealer.DealerCode;
            DealerHierarchy = dealerHierarchy;
        }

        public virtual void UpdateLocalization(Dealer dealer)
        {
            dealer.DealerLocalization
                  .ToList()
                  .ForEach(dealerLocalization =>
                  {
                      if (DealerLocalization.ContainsKey(dealerLocalization.Key))
                      {
                          DealerLocalization[dealerLocalization.Key].Update(dealerLocalization.Value.ToString());
                      }
                      else
                      {
                          DealerLocalization.Add(dealerLocalization.Key, new DealerLocalization(dealerLocalization.Value.ToString()));
                      }
                  });
        }

        public virtual void UpdateDealerLocation(decimal longitude, decimal latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }



        public virtual void Activate()
        {
            SetStatus(StatusTypeEnum.Available);
        }

        public virtual void Passivate()
        {
            SetStatus(StatusTypeEnum.NotAvailable);
        }

        public virtual void Delete()
        {
            DeleteMark("DealerCode");

            SetStatus(StatusTypeEnum.Deleted);
        }

        #region Private Methods

        private void ExpandSubDealersRecursively(Dealer dealer, ICollection<Dealer> dealers)
        {
            dealers.Add(dealer);
            dealer.SubDealers.ToList().ForEach(x => ExpandSubDealersRecursively(x, dealers));
        }

        #endregion
    }
}
