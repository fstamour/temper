
: nop, 0 @assembly ;

: sleep, $9588 @assembly ;

\ Watchdog Reset
: wdr, n( % 1001 0101 1010 1000 ) @assembly ;

\ Clear Carry Flag
: clc, n( % 1001 0100 1000 1000 ) @assembly ;
\ Set Carry Flag
: sec, n( % 1001 0100 0000 1000 ) @assembly ;

\ Clear Half Carry Flag
: clh, n( % 1001 0100 1101 1000 ) @assembly ;
\ Set Half Carry Flag
: seh, n( % 1001 0100 0101 1000 ) @assembly ;

\ Clear Global Interrupt Enable bit
: cli, n( % 1001 0100 1111 1000 ) @assembly ;
\ Set Global Interrupt Enable Bit
: sei, n( % 1001 0100 0111 1000 ) @assembly ;

\ Clear Negative Flag
: cln, n( % 1001 0100 1010 1000 ) @assembly ;
\ Set  Negative Flag
: sen, n( % 1001 0100 0010 1000 ) @assembly ;

\ Clear Zero Flag
: clz, n( % 1001 0100 1001 1000 ) @assembly ;
\ Set Zero Flag
: sez, n( % 1001 0100 0001 1000 ) @assembly ;

\ Clear Sign Flag
: cls, n( % 1001 0100 1100 1000 ) @assembly ;
\ Set Sign Flag
: ses, n( % 1001 0100 0100 1000 ) @assembly ;

\ Clear Overflow Flag
: clv, n( % 1001 0100 1011 1000 ) @assembly ;
\ Set Overflow Flag
: sev, n( % 1001 0100 0011 1000 ) @assembly ;

\ Clear T Bit
: clt, n( % 1001 0100 1110 1000 ) @assembly ;
\ Set T Bit
: set, n( % 1001 0100 0110 1000 ) @assembly ;

\ Indirect Jump
: ijmp, n( % 1001 0100 0000 1001 ) @assembly ;
\ Extended Indirect Jump
: eijmp, n( % 1001 0100 0001 1001 ) @assembly ;
\ Indirect Call to Subroutine
: icall, n( % 1001 0101 0000 1001 ) @assembly ;
\ Extended Indirect Call to Subroutine
: iecall, n( % 1001 0101 0001 1001 ) @assembly ;
\ Return from subroutine
: ret, n( % 1001 0101 0000 1000 ) @assembly ;
\ Return from Interrupt
: reti, n( % 1001 0101 0001 1000 ) @assembly ;

\ TODO lpm has 2 other variants, this one loads the value into R0
\ Load Program Memory
: lpm, n( % 1001 0101 1100 1000 ) @assembly ;

\ TODO elpm has 2 other variants, this one loads the value into R0
\ Extended Load Program Memor
: elpm>r0, n( % 1001 0101 1101 1000 ) @assembly ;

: rd-layout ( d r -- u )
  \ 0000 01rd dddd rrrr
  dup $f and swap \ low r
  $10 and 6 lshift \ high r
  or swap
  4 lshift ( d ) ;

\ Compare with Carry
: cpc, ( d r -- )
  \ 0000 01rd dddd rrrr
  rd-layout $0400 or @assembly ;

\ Load immediate
: ldi, ( d k -- )
  \ 1110 kkkk dddd kkkk
  dup $f and swap \ low kkkk
  $f0 and 4 lshift or \ high kkkk
  swap 16 - \ d is between 16 and 32
  4 lshift \ move d in place
  or $E000 or @assembly ;

: out, ( A d -- )
  \ 1011 1AAd dddd AAAA
  %11111 and 4 lshift swap
  dup %110000 and 5 lshift swap
  $f and or
  or $B800 or @assembly ;
