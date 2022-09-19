\ instructions with 0 operands
s" oooo oooo oooo oooo" @layout

s" No Operation"
  0
  instruction: nop,

s" Sleep"
  n( % 1001 0101 1000 1000 )
  instruction: sleep,

s" Watchdog Reset"
  n( % 1001 0101 1010 1000 )
  instruction: wdr,


s" Clear Carry Flag"
  $9488
  instruction: clc,

s" Set Carry Flag"
  n( % 1001 0100 0000 1000 )
  instruction: sec,

s" Clear Half Carry Flag"
  n( % 1001 0100 1101 1000 )
  instruction: clh,

s" Set Half Carry Flag"
  n( % 1001 0100 0101 1000 )
  instruction: seh,

s" Clear Global Interrupt Enable bit"
  $94F8
  instruction: cli,

s" Set Global Interrupt Enable Bit"
  n( % 1001 0100 0111 1000 )
  instruction: sei,

s" Clear Negative Flag"
  n( % 1001 0100 1010 1000 )
  instruction: cln,

s" Set Negative Flag"
  n( % 1001 0100 0010 1000 )
  instruction: sen,

s" Clear Zero Flag"
  n( % 1001 0100 1001 1000 )
  instruction: clz,

s" Set Zero Flag"
  n( % 1001 0100 0001 1000 )
  instruction: sez,

s" Clear Sign Flag"
  n( % 1001 0100 1100 1000 )
  instruction: cls,

s" Set Sign Flag"
  n( % 1001 0100 0100 1000 )
  instruction: ses,

s" Clear Overflow Flag"
  n( % 1001 0100 1011 1000 )

s" Set Overflow Flag"
  n( % 1001 0100 0011 1000 )
  instruction: sev,

s" Clear T Bit"
  n( % 1001 0100 1110 1000 )
  instruction: clt,

s" Set T Bit"
  n( % 1001 0100 0110 1000 )
  instruction: set,


s" Indirect Jump"
  n( % 1001 0100 0000 1001 )
  instruction: ijmp,

s" Extended Indirect Jump"
  n( % 1001 0100 0001 1001 )
  instruction: eijmp,

s" Indirect Call to Subroutine"
  n( % 1001 0101 0000 1001 )
  instruction: icall,

s" Extended Indirect Call to Subroutine"
  n( % 1001 0101 0001 1001 )
  instruction: iecall,

s" Return from subroutine"
  n( % 1001 0101 0000 1000 )
  instruction: ret,

s" Return from Interrupt"
  n( % 1001 0101 0001 1000 )
  instruction: reti,

\ TODO elpm has 2 other variants, this one loads the value into R0
s" Extended Load Program Memory"
  n( % 1001 0101 1101 1000 )
  instruction: elpm>r0,


(
all the instructions without
operands:
ijmp [x] eijmp [x]
icall [x] eicall [x]
ret [x] reti [x]
elmp [x]
sec [x] clc [x]
sen [x] cln [x]
sez [x] clz [x]
sei [x] cli [x]
ses [x] cls [x]
sev [x] clv [x]
set [x] clt [x]
seh [x] clh [x]
nop [x]
sleep [x]
wdr [x]
)


s" oooo oord dddd rrrr" @layout

\ 0000 01 cpc, Compare with Carry
\ 0000 10 sbc, Subtract with Carry

s" Add without Carry"
  n( % 0000 11 )
  instruction: add,

\ 0001 00 cpse, Compare Skip if Equal
\ 0001 01 cp, Compare
\ 0001 10 sub, Subtract without Carry

s" Add with Carry"
  n( % 0001 11 )
  instruction: adc,


s" Logical AND"
  n( % 0010 00 )
  instruction: and,

\ 0010 01 eor, Exclusive Or
\ 0010 10 or, Logical Or
\ 0010 11 mov, Copy Register

\ 1001 11 mul, Multiply Unsigned



s" oooo oooo kkdd kkkk" @layout

s" Add Immediate to Word"
  n( % 1001 0110 )
  instruction: adiw,
\ cbr is a subset of adiw

s" oooo kkkk dddd kkkk" @layout

s" Logical AND with Immediate"
  n( % 0111 )
  instruction: andi,
\ cpi Compare with Immediate

s" oooo oood dddd oooo" @layout

s" Arithmetic Shift Right"
  n( % 1001 010 0101 )
  instruction: asr,


s" oooo oooo osss oooo" @layout

s" Bit Clear in SREG"
  n( % 1001 0100 1 1000 )
  instruction: bclr,

s" Bit Set in SREG"
  n( % 1001 0100 0 1000 )
  instruction: bset,


s" oooo oood dddd obbb" @layout

s" Bit Load from the T Bit in SREG to a Bit in Register"
  n( % 1111 100 0 )
  instruction: bld,

s" Bit Store from Bit in Register to T Bit in SREG"
  n( % 1111 101 0 )
  instruction: bst,


s" oooo ookk kkkk ksss" @layout

s" Branch if Bit in SREG is Cleared"
  n( % 1111 01 )
  instruction: brbc,

s" Branch if Bit in SREG is Set"
  n( % 1111 00 )
  instruction: brbs,

(

\ The following instructions are
\ equivalent to brbc or brbs with
\ specific values of s

brbc
====
1111 01 000 brcc brsh
1111 01 001 brne
1111 01 011 brvs
1111 01 010 brpl
1111 01 100 brge
1111 01 101 brhc
1111 01 110 brtc
1111 01 111 brid

brbs
====
1111 00 000 brcs brlo
1111 00 001 breq
1111 00 010 brmi
1111 00 011 brvc
1111 00 100 brlt
1111 00 101 brhs
1111 00 110 brts
1111 00 111 brie
)

(
\ 32bits instructions!!!
oooo oook kkkk oook
kkkkk kkkk kkkk kkkkk
1001 010 111 call
1001 010 110 jmp

oooo oood dddd oooo
kkkkk kkkk kkkk kkkkk
1001 000 0000 lds
1001 001 0000 sts
)


(

\ Some instructions have variants:
- elpm has 3 variants

)