import { NumberUtils } from '@/core/utils/number-utils';
import { Filter } from '@/core/ui/common/filter';

/**
 * Formatter to convert large numbers to be rouneded with K.
 * EX: 12320 -> 12.3K
 */
export const shortHand: Filter = (input: string) => NumberUtils.formatWithK(Number.parseInt(input, 10));
