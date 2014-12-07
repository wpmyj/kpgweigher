/* CH374оƬ Ӳ������� V1.0 */
/* �ṩI/O�ӿ��ӳ���,�ṩ�Ĵ�������д�ӳ��� */

#include	"sjDefine.h"
#include	"CH374INC.H"

#ifndef	__CH374_HAL_H__
#define __CH374_HAL_H__

#ifdef __CX51__
#ifndef __C51__
#define __C51__		1
#endif
#endif

#ifdef __C51__
typedef const unsigned char code		UINT8C;
typedef unsigned char volatile xdata	UINT8XV;
#else
typedef const unsigned char		UINT8C;
typedef unsigned char volatile	UINT8XV;
#endif

#ifdef __C51__
#define	BIG_ENDIAN		1
#endif


//#define CH374_INT_WIRE			INT0	/* �ٶ�CH374��INT#����,���δ������ôҲ����ͨ����ѯ�жϱ�־�Ĵ���ʵ�� */


// ���ӵ�USB����״̬����
#define		ERR_USB_UNKNOWN		0xFA		// δ֪����,��Ӧ�÷��������,����Ӳ�����߳������


/* ��ʱָ��΢��ʱ��,���ݵ�Ƭ����Ƶ����,����ȷ */
void	mDelayuS( UINT8 us );

/* ��ʱָ������ʱ�� */
void	mDelaymS( UINT8 ms );

void	CH374_PORT_INIT( );  /* CH374�ӿڳ�ʼ�� */

UINT8	Read374Byte( UINT8 mAddr );	// ��ָ���Ĵ�����ȡ����

void	Write374Byte( UINT8 mAddr, UINT8 mData );	// ��ָ���Ĵ���д������

void	Read374Block( UINT8 mAddr, UINT8 mLen, PUINT8 mBuf );  /* ��ָ����ʼ��ַ�������ݿ� */

void	Write374Block( UINT8 mAddr, UINT8 mLen, PUINT8 mBuf );  /* ��ָ����ʼ��ַд�����ݿ� */

void	Spi374Start( UINT8 addr, UINT8 cmd );  /* SPI��ʼ */
//void	Spi374OutByte( UINT8 d ); 
void    MySpi374Stop(void);	
// ��ѯCH374�ж�(INT#�͵�ƽ)
BOOL	Query374Interrupt( void );

// �ȴ�CH374�ж�(INT#�͵�ƽ)����ʱ�򷵻�ERR_USB_UNKNOWN
UINT8	Wait374Interrupt( void );

#endif