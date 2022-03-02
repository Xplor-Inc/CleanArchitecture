import dateFormat from "dateformat";

const Sum = (obj: any[], property: string) => {
    var total = 0
    for (var i = 0; i < obj.length; i++) {
        total += obj[i][property]
    }
    return parseInt(total.toFixed(2));
}

const groupBy = (obj: any[], key: string) => {
    var items = [] as string[]
    for (var i = 0; i < obj.length; i++) {
        var item = obj[i][key]
        if (!items.includes(item)) {
            items.push(item);
        }
    }
    return items;
}

const _dateForm = (_date?: string) => {
    if (!_date) {
        return null;
    }
    return new Date(new Date(_date.toString()).getTime())
}

const _dd_mmm = (_date?: Date | null | undefined | string) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "dd mmm");
}

const _mmm_YYYY = (_date?: Date | null | undefined | string) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "mmmm yyyy");
}

const _dd_mmm_yyyy = (_date?: Date | null | undefined | string) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "dd mmm yyyy");
}

const _hh_mm_ss_tt = (_date?: Date | null | undefined) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "hh:MM:ss TT");
}

const _dd_mmm_yy_HH_mm_ss = (_date?: Date | null | undefined) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "dd mmm yy HH:MM:ss");
}

const _yyyy__mm_dd_ = (_date?: Date | null | undefined) => {
    if (!_date) {
        return '';
    }
    return dateFormat(_date, "yyyy-mm-dd") + "T00:00:00";
}

const _isValid = (prop?: string) => {
    if (!prop || prop.length === 0 || prop.trimEnd().length === 0)
        return false;
    return true;
}

export const Utility = {
    GroupBy: groupBy,
    Sum: Sum,
    Validate: {
        Date: _dateForm,
        String:_isValid
    },
    Format: {
        Date_DD_MMM: _dd_mmm,
        Date_DD_MMM_YYYY: _dd_mmm_yyyy,
        Time_HH_MM_SS_TT: _hh_mm_ss_tt,
        DateTime_DD_MMM_YY_HH_MM_SS: _dd_mmm_yy_HH_mm_ss,
        Date_YYYY_MM_DD: _yyyy__mm_dd_,
        Date_MMMM_YYYY:_mmm_YYYY
    }
}