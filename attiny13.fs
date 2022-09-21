
0 value ramstart
0 value ramend

0 value r0
1 value r1
2 value r2
3 value r3
4 value r4
5 value r5
6 value r6
7 value r7
8 value r8
9 value r9
10 value r10
11 value r11
12 value r12
13 value r13
14 value r14
15 value r15
16 value r16
17 value r17
18 value r18
19 value r19
20 value r20
21 value r21
22 value r22
23 value r23
24 value r24
25 value r25
26 value r26
27 value r27
28 value r28
29 value r29
30 value r30
31 value r31
32 value r32

$3F value SREG
$3D value SPL
$3B value GIMSK
$3A value GIFR
$39 value TIMSK0
$38 value TIFR0
$37 value SPMCSR
$36 value OCR0A
$35 value MCUCR
$34 value MCUSR
$33 value TCCR0B
$32 value TCNT0
$31 value OSCCAL
$2F value TCCR0A
$2E value DWDR
$29 value OCR0B
$28 value GTCCR
$26 value CLKPR
$21 value WDTCR
$1E value EEARL
$1D value EEDR
$1C value EECR
$18 value PORTB
$17 value DDRB
$16 value PINB
$15 value PCMSK
$14 value DIDR0
$08 value ACSR
$07 value ADMUX
$06 value ADCSRA
$05 value ADCH
$04 value ADCL
$03 value ADCSRB

: attiny13
 \ 1K of program memory
 1024 0assembly
 $60 to ramstart
 $9f to ramend
 ;


attiny13