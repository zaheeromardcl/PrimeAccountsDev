using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PrimeActs.Domain.ViewModels
{
    public sealed class NatWestStdDomesticPaymentCSV
    {
        [MaxLength(16)]
        public string H001 { get; set; } // Originating Customer identifier
        [MaxLength(50)]
        public string H002 { get; set; } // Import file name
        [MaxLength(4)]
        public string H003 { get; set; } // Bank Identifier
        [Range(1, 9)]
        public int T001 { get; set; } // Record Type
        [MaxLength(1)]
        public string T002 { get; set; } // Template Indicator
        [MaxLength(20)]
        public string T003 { get; set; } // Template Reference
        [MaxLength(1)]
        public string T004 { get; set; } // Confidential Indicator
        [MaxLength(25)]
        public string T005 { get; set; } // Beneficiary Identifier
        [MaxLength(18)]
        public string T006 { get; set; } // Customer payment reference
        [MaxLength(2)]
        public string T007 { get; set; } // Destination Country
        [MaxLength(1)]
        public string T008 { get; set; } // Priority
        [MaxLength(1)]
        public string T009 { get; set; } // Routing Method
        [MaxLength(34)]
        public string T010 { get; set; } // Debit Account Identifier 
        [MaxLength(34)]
        public string T011 { get; set; } // Debit Charges Account Identifier 
        [MaxLength(3)]
        public string T012 { get; set; } // Charges Code Type
        [MaxLength(3)]
        public string T013 { get; set; } // Payment currency
        public decimal? T014 { get; set; } // Payment amount
        [MaxLength(8)]
        public string T015 { get; set; } // Execution Date
        [MaxLength(8)]
        public string T016 { get; set; } // Payment Arrival Date
        [MaxLength(16)]
        public string T017 { get; set; } // Ordering Institution Identifier
        [MaxLength(35)]
        public string T018 { get; set; } // Ordering Institution Name and Address Line 1
        [MaxLength(35)]
        public string T019 { get; set; } // Ordering Institution Name and Address Line 2
        [MaxLength(35)]
        public string T020 { get; set; } // Ordering Institution Name and Address Line 3
        [MaxLength(35)]
        public string T021 { get; set; } // Ordering Institution Name and Address Line 4
        [MaxLength(16)]
        public string T022 { get; set; } // Account with Bank Identifier
        [MaxLength(34)]
        public string T023 { get; set; } // Account with Bank Account Number
        [MaxLength(35)]
        public string T024 { get; set; } // Account with Bank Name and Address Line 1
        [MaxLength(35)]
        public string T025 { get; set; } // Account with Bank Name and Address Line 2
        [MaxLength(35)]
        public string T026 { get; set; } // Account with Bank Name and Address Line 3
        [MaxLength(35)]
        public string T027 { get; set; } // Account with Bank Name and Address Line 4        
        [MaxLength(34)]
        public string T028 { get; set; } // Beneficiary Account Number
        [MaxLength(16)]
        public string T029 { get; set; } // Beneficiary Institution Identifier
        [MaxLength(35)]
        public string T030 { get; set; } // Beneficiary Name and Address Line 1
        [MaxLength(35)]
        public string T031 { get; set; } // Beneficiary Name and Address Line 2
        [MaxLength(35)]
        public string T032 { get; set; } // Beneficiary Name and Address Line 3
        [MaxLength(35)]
        public string T033 { get; set; } // Beneficiary Name and Address Line 4
        [MaxLength(18)]
        public string T034 { get; set; } // Beneficiary Reference
        [MaxLength(16)]
        public string T035 { get; set; } // FX Deal Reference       
        public decimal? T036 { get; set; } // FX Deal Exchange Rate
        [MaxLength(35)]
        public string T037 { get; set; } // Information for the Beneficiary Line Number 1
        [MaxLength(35)]
        public string T038 { get; set; } // Information for the Beneficiary Line Number 2
        [MaxLength(35)]
        public string T039 { get; set; } // Information for the Beneficiary Line Number 3
        [MaxLength(35)]
        public string T040 { get; set; } // Information for the Beneficiary Line Number 4
        [MaxLength(1)]
        public string T041 { get; set; } // RTGS
        [MaxLength(3)]
        public string T042 { get; set; } // Credit Currency
        [MaxLength(16)]
        public string T043 { get; set; } // Intermediary
        [MaxLength(35)]
        public string T044 { get; set; } // Intermediary Bank Name and Address Line 1
        [MaxLength(35)]
        public string T045 { get; set; } // Intermediary Bank Name and Address Line 2
        [MaxLength(35)]
        public string T046 { get; set; } // Intermediary Bank Name and Address Line 3
        [MaxLength(35)]
        public string T047 { get; set; } // Intermediary Bank Name and Address Line 4
        [MaxLength(4)]
        public string T048 { get; set; } // Additional Code Words number 1
        [MaxLength(29)]
        public string T049 { get; set; } // Additional Code Words text number 1
        [MaxLength(4)]
        public string T050 { get; set; } // Additional Code Words number 2
        [MaxLength(29)]
        public string T051 { get; set; } // Additional Code Words text number 2
        [MaxLength(4)]
        public string T052 { get; set; } // Additional Code Words number 3
        [MaxLength(29)]
        public string T053 { get; set; } // Additional Code Words text number 3
        [MaxLength(4)]
        public string T054 { get; set; } // Additional Code Words number 4
        [MaxLength(29)]
        public string T055 { get; set; } // Additional Code Words text number 4
        [MaxLength(4)]
        public string T056 { get; set; } // Additional Code Words number 5
        [MaxLength(29)]
        public string T057 { get; set; } // Additional Code Words text number 5
        [MaxLength(4)]
        public string T058 { get; set; } // Additional Code Words number 6
        [MaxLength(29)]
        public string T059 { get; set; } // Additional Code Words text number 6
        [MaxLength(4)]
        public string T060 { get; set; } // Additional Code Words number 7
        [MaxLength(29)]
        public string T061 { get; set; } // Additional Code Words text number 7
        [MaxLength(4)]
        public string T062 { get; set; } // Additional Code Words number 8
        [MaxLength(29)]
        public string T063 { get; set; } // Additional Code Words text number 8
        [MaxLength(4)]
        public string T064 { get; set; } // Additional Code Words number 9
        [MaxLength(29)]
        public string T065 { get; set; } // Additional Code Words text number 9
        [MaxLength(4)]
        public string T066 { get; set; } // Additional Code Words number 10
        [MaxLength(29)]
        public string T067 { get; set; } // Additional Code Words text number 10
        [MaxLength(35)]
        public string T068 { get; set; } // Regulatory Reporting Line Number 1
        [MaxLength(35)]
        public string T069 { get; set; } // Regulatory Reporting Line Number 2
        [MaxLength(35)]
        public string T070 { get; set; } // Regulatory Reporting Line Number 3
        [MaxLength(1)]
        public string T071 { get; set; } // Remittance Advice Indicator
        [MaxLength(35)]
        public string T072 { get; set; } // Remittance Advice Beneficiary Address Line 1
    }
}

