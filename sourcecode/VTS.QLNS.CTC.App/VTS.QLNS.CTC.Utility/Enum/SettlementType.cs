namespace VTS.QLNS.CTC.Utility.Enum
{
    public enum SettlementAction
    {
        ADD_SETTLEMENT_VOUCHER = 0,
        EDIT_SETTLEMENT_VOUCHER = 1
    }

    public struct SettlementScreen
    {
        public const string ROOT_DIALOG = "RootDialog";
        public const string DETAIL_DIALOG = "DetailDialog";
        public const string REGULAR_BUDGET_DETAIL_DIALOG = "RegularBudgetDetailDialog";
        public const string STATE_BUDGET_DETAIL_DIALOG = "StateBudgetDetailDialog";
        public const string VOUCHER_LIST_DETAIL_DIALOG = "VoucherListDetailDialog";
        public const string DEFENSE_BUDGET_DETAIL_DIALOG = "DefenseBudgetDetailDialog";
        public const string EXPENSE_BUDGET_DETAIL_DIALOG = "ExpenseBudgetDetailDialog";
        public const string FOREX_BUDGET_DETAIL_DIALOG = "ForexBudgetDetailDialog";
        public const string REAL_REVENUE_EXPENDITURE_DETAIL_DIALOG = "RealRevenueExpenditureDialog";
    }

    public struct SettlementType
    {
        public const string REGULAR_BUDGET = "101";
        public const string DEFENSE_BUDGET = "1";
        public const string STATE_BUDGET = "2";
        public const string FOREX_BUDGET = "3";
        public const string EXPENSE_BUDGET = "4";
        public const string LNS = "101,1,2,3,4";
    }

    public enum Status
    {
        DELETE = 0,
        ACTIVE = 1
    }

    public enum SettlementPrintType
    {
        PRINT_COMMUNICATE_SETTLEMENT_LNS = 1,
        PRINT_COMMUNICATE_SETTLEMENT_AGENCY = 2,
        PRINT_REGULARLY_SETTLEMENT = 3,
        PRINT_DEFENSE_SETTLEMENT = 4,
        PRINT_STATE_SETTLEMENT = 5,
        PRINT_FOREX_SETTLEMENT = 6,
        PRINT_EXPENSE_SETTLEMENT = 7,
        PRINT_LNS_SETTLEMENT = 8,
        PRINT_SUMMARY_AGENCY = 9,
        PRINT_ESTIMATE_SETTLEMENT = 10,
        PRINT_SUMMARY_YEAR_SETTLEMENT = 11,
        PRINT_REPORT_PUBLIC_SETTLEMENT = 12,
        PRINT_YEAR_SETTLEMENT_SUMARY_BUBGET = 13,
        PRINT_RECEIVE_SETTLEMENT
    }

    public enum SettlementVoucherDetailDisplay
    {
        ALL = 1,
        HAVE_ESTIMATE = 2,
        HAVE_ESTIMATE_EXISTENCE = 3,
        HAVE_ESTIMATE_SETTLEMENT_EXISTENCE = 4,
        ENTERED_THE_SETTLEMENT = 5
    }

    public enum SettlementOrdinal
    {
        DIAGRAM = 0,
        REGULAR_BUDGET = 1,
        DEFENSE_BUDGET = 2,
        STATE_BUDGET = 3,
        VOUCHER_LIST = 4,
        ARMY_VOUCHER = 5
    }

    public enum ArmyVoucherDetailMethod
    {
        GET_PART = 0,
        GET_ALL = 1
    }

    public enum Censorship
    {
        NEW = 0,
        PENDING = 1,
        DENY = 2,
        ACCEPT = 3,
        AGGREGATE = 4
    }

    public struct FileExtension
    {
        public const string EXCEL = ".xlsx";
        public const string PDF = ".pdf";
    }

    public enum ArmyPrintType
    {
        PRINT_ARMY = 0,
        PRINT_ARMY_UP_DOWN = 1,
        PRINT_ARMY_AVERAGE = 2,
        PRINT_ARMY_REGULAR = 3,
        PRINT_ARMY_LEAVE = 4,
        PRINT_ARMY_JURISPRUDENCE = 5
    }

    public enum VoucherListPrintType
    {
        PRINT_VOUCHER_LIST = 0,
        PRINT_SUMMARY_VOUCHER_LIST = 1
    }

    public enum ArmyExportCheckType
    {
        EXPORT_AGGREGATE_TYPE = 1,
        EXPORT_UNIT_TYPE = 2,
        EXPORT_UNITS_TYPE = 3,

    }
}
