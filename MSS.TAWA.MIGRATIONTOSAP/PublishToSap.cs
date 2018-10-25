using MSS.TAWA.HP;
using MSS.TAWA.MODEL;
using Newtonsoft.Json;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSS.TAWA.MIGRATIONTOSAP
{
    public class PublishToSap
    {
        public void PublishRendicionesToSap(FacturasWebMigracion facturasWebMigracion)
        {
            var company = new SAPbobsCOM.Company();
            try
            {
                company = GetAndConnectCompany();
                company.StartTransaction();
                PublishDocumentToSAP(company, facturasWebMigracion);

                if (company.InTransaction)
                    company.EndTransaction(BoWfTransOpt.wf_Commit);

                company.Disconnect();
            }
            catch (SapException)
            {
                var error = "Mensaje SAP: " + company.GetLastErrorCode() + "- " + company.GetLastErrorDescription();
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                if (company.Connected)
                    if (company.InTransaction)
                        company.EndTransaction(BoWfTransOpt.wf_RollBack);
                throw;
            }
        }

        public void PublishRendicionesToSap(List<MaestroTrabajadores> businessPartnerList, List<FacturasWebMigracion> facturasWebMigracions)
        {
            var company = new SAPbobsCOM.Company();
            try
            {
                company = GetAndConnectCompany();
                company.StartTransaction();
                businessPartnerList.ForEach(x => PublishBusinessPartnerToSAP(company, x));
                facturasWebMigracions.ForEach(x => PublishDocumentToSAP(company, x));

                if (company.InTransaction)
                    company.EndTransaction(BoWfTransOpt.wf_Commit);

                company.Disconnect();
            }
            catch (SapException sapException)
            {
                var error = company.GetLastErrorDescription() + sapException.ToString();
                throw new Exception(error);
            }
            catch (Exception ex)
            {
                if (company.InTransaction)
                    company.EndTransaction(BoWfTransOpt.wf_RollBack);
                throw;
            }
        }


        #region PublishToSAP

        private bool PublishBusinessPartnerToSAP(Company company, MaestroTrabajadores bp)
        {
            SAPbobsCOM.BusinessPartners businessPartner = (SAPbobsCOM.BusinessPartners)company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

            businessPartner.CardCode = bp.CardCode;
            businessPartner.CardName = bp.CardName;
            businessPartner.CardType = SAPbobsCOM.BoCardTypes.cSupplier;
            businessPartner.FederalTaxID = bp.LicTradNum;
            businessPartner.DebitorAccount = bp.DebitAccount;
            businessPartner.GroupCode = (int)bp.GroupCode;
            businessPartner.Address = bp.Address;
            businessPartner.Phone1 = bp.Phone1;
            businessPartner.Phone2 = bp.Phone2;
            businessPartner.Cellular = bp.Cellular;
            businessPartner.EmailAddress = bp.E_Mail;
            businessPartner.Currency = "##";

            if (bp.U_BankAcct != string.Empty)
            {
                businessPartner.BPBankAccounts.AccountNo = bp.U_BankAcct;
                businessPartner.BPBankAccounts.BankCode = bp.U_BankCode;
                businessPartner.BPBankAccounts.Country = "PE";
                businessPartner.BPBankAccounts.Add();
            }

            return businessPartner.Add() == 0;
        }

        private void PublishDocumentToSAP(Company company, FacturasWebMigracion facturaMigrada)
        {
            int etapa = facturaMigrada.Etapa ?? 0;

            SAPbobsCOM.Documents invoice;
            int docSubType = facturaMigrada.DocSubType ?? 0;
            switch (docSubType)
            {
                case 18:
                case 181:
                default:
                    invoice = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                    break;
                case 19:
                    invoice = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes);
                    break;
            }

            invoice.Indicator = facturaMigrada.U_BPP_MDTD;
            invoice.ControlAccount = facturaMigrada.ControlAccount;
            if (facturaMigrada.Series != 0 && facturaMigrada.Series != null)
                invoice.Series = facturaMigrada.Series.Value;
            invoice.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service;
            invoice.CardCode = facturaMigrada.CardCode;
            invoice.DocCurrency = facturaMigrada.DocCurrency;
            invoice.JournalMemo = facturaMigrada.JournalMemo;
            invoice.Comments = facturaMigrada.Asunto;
            invoice.PaymentMethod = facturaMigrada.PaymentMethod;
            invoice.NumAtCard = facturaMigrada.NumAtCard;
            invoice.FolioPrefixString = facturaMigrada.FolioPref;
            invoice.FolioNumber = facturaMigrada.FolioNum ?? 0;
            invoice.DocDate = facturaMigrada.DocDate ?? throw new Exception();
            invoice.DocDueDate = facturaMigrada.DocDueDate ?? throw new Exception();
            invoice.TaxDate = facturaMigrada.TaxDate ?? throw new Exception();

            //CAMPOS DE USUARIO
            try { invoice.UserFields.Fields.Item("U_MSSL_TOP").Value = "02"; }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            try { invoice.UserFields.Fields.Item("U_MSSL_TBI").Value = "A"; }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            try { invoice.UserFields.Fields.Item("U_ExCode").Value = facturaMigrada.ExCode.ToString(); }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            try { invoice.UserFields.Fields.Item("U_Etapa").Value = etapa.ToString(); }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            try { invoice.UserFields.Fields.Item("U_WebType").Value = (facturaMigrada.TipoDocumento ?? 0).ToString(); }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            try { if (facturaMigrada.FolioPref == TipoDocumentoSunat.ReciboDeHonorarios.GetCodigoSunat()) invoice.UserFields.Fields.Item("U_MSSL_TBI").Value = "R"; }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }


            //DOCUMENT LINES
            invoice.Lines.AccountCode = facturaMigrada.AccountCode;
            invoice.Lines.TaxCode = facturaMigrada.TaxCode;
            invoice.Lines.LineTotal = Convert.ToDouble(facturaMigrada.LineTotal);
            invoice.Lines.CostingCode = facturaMigrada.CostingCode;
            invoice.Lines.CostingCode2 = facturaMigrada.CostingCode2;
            invoice.Lines.CostingCode3 = facturaMigrada.CostingCode3;
            invoice.Lines.CostingCode4 = facturaMigrada.CostingCode4;
            invoice.Lines.CostingCode5 = facturaMigrada.CostingCode5;
            invoice.Lines.ItemDescription = facturaMigrada.Description;

            try
            { invoice.Lines.UserFields.Fields.Item("U_MSS_ORD").Value = facturaMigrada.U_MSS_ORD; }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }


            invoice.Lines.Add();
            //company.StartTransaction();

            if (invoice.Add() == 0)
            {
                int newDocEntry = int.Parse(company.GetNewObjectKey());
                bool isInvoice = (docSubType != 19);

                if (etapa == 2)
                    PayInvoice(company, newDocEntry, isInvoice, facturaMigrada.Code, facturaMigrada.AccountCode);
            }
            else
                throw new SapException();
        }

        private void PayInvoice(SAPbobsCOM.Company company, int docEntry, bool isInvoice, String _aperturaCodigo, string _aperturaAccountCode)
        {
            SAPbobsCOM.Payments payment = company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
            SAPbobsCOM.Documents doc = isInvoice
                ? company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices)
                : company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseCreditNotes);
            doc.GetByKey(docEntry);

            payment.CardCode = doc.CardCode;
            payment.DocDate = doc.DocDate;
            payment.TaxDate = doc.TaxDate;
            payment.DueDate = doc.DocDueDate;
            payment.TransferDate = doc.DocDate;
            payment.DocCurrency = doc.DocCurrency;
            payment.CounterReference = _aperturaCodigo;
            payment.TransferAccount = _aperturaAccountCode;
            payment.Remarks = doc.JournalMemo;
            payment.JournalRemarks = doc.JournalMemo;

            payment.Invoices.InvoiceType = isInvoice
                ? SAPbobsCOM.BoRcptInvTypes.it_PurchaseInvoice
                : SAPbobsCOM.BoRcptInvTypes.it_PurchaseCreditNote;
            payment.Invoices.DocEntry = docEntry;

            try
            { payment.UserFields.Fields.Item("U_MSSL_TMP").Value = "008"; }
            catch (Exception ex) { ExceptionHelper.LogException(ex); }

            switch (doc.DocCurrency)
            {
                case "SOL":
                case "S/":
                case "S/.":
                case "sol":
                case "SOLES":
                case "soles":
                    payment.Invoices.SumApplied = doc.DocTotal;
                    payment.TransferSum = doc.DocTotal;
                    break;
                default:
                    payment.Invoices.AppliedFC = doc.DocTotalFc;
                    payment.TransferSum = doc.DocTotalFc;
                    break;
            }
            if (payment.Add() != 0)
                throw new SapException();
        }

        private Company GetAndConnectCompany()
        {
            var configValues = new SICER_WEBEntities().CONFIG;
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();
            company.CompanyDB = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_DBCompany)?.Valor;
            company.Server = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_Server)?.Valor;
            company.DbUserName = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_DBUser)?.Valor;
            company.DbPassword = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_DBPassword)?.Valor;
            company.UserName = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_SBOUser)?.Valor;
            company.Password = configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_SBOPassword)?.Valor;
            company.language = SAPbobsCOM.BoSuppLangs.ln_Spanish;
            company.DbServerType = (SAPbobsCOM.BoDataServerTypes)Enum.Parse(typeof(SAPbobsCOM.BoDataServerTypes), configValues.FirstOrDefault(x => x.Llave == ConstantHelper.CONFIGKEYS.Company_DbServerType)?.Valor);
            if (company.Connect() == 0)
                return company;
            else
                throw new SapException();
        }

        #endregion

    }
}
