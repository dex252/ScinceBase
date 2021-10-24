import 'jquery-mask-plugin';

export function SetMask(input, mask) {
    input.mask(mask);   
}

export function SetDigitMask(input, mask, pattern) {
    input.mask(mask, {
        translation: {
          'Z': {
            pattern: pattern
          }
        }
      });
    
}

export function ValidDigitDifference(input1, input2){
    let value1 = Number(input1.val());
    let value2 = Number(input2.val());

    if(value1 >= value2){
        return false;
    }

    return true;
}

export function DigitCompare(input){
  let min = input.data('min');
  let max = input.data('max');

  let value = input.val();

  if(value < min){
    input.val(min);
  }

  if(value > max){
    input.val(max);
  }
  
}