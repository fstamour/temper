\ instructions with 0 operands
s" oooo oooo oooo oooo" @layout

s" Return from subroutine"
  $9508
  instruction: ret,

s" Clear Carry Flag"
  $9488 
  instruction: clc,

s" Clear Half Carry Flag"
  $94D8
  instruction: clh,

s" Clear Global Interrupt Enable bit"
  $94F8
  instruction: cli,

( 
all the instructions without
operands:
ijmp [ ] eijmp [ ]
icall [ ] eicall [ ]
ret [x] reti [ ]
elmp [ ]
sec [ ] clc [x]
sen [ ] cln
sez [ ] clz [ ]
sei [ ] cli [x]
ses [ ] cls [ ]
sev [ ] clv [ ]
set [ ] clt [ ]
seh [ ] clh [x]
break [ ]
nop [ ]
sleep [ ]
wdr [ ]
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





