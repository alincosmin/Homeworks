/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 5.2.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPlainTextEdit>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QPushButton *ButtonGetDoc;
    QPlainTextEdit *TextboxDocContent;
    QLineEdit *InputDocName;
    QPushButton *ButtonCreate;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName(QStringLiteral("MainWindow"));
        MainWindow->resize(378, 306);
        centralWidget = new QWidget(MainWindow);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName(QStringLiteral("gridLayout"));
        ButtonGetDoc = new QPushButton(centralWidget);
        ButtonGetDoc->setObjectName(QStringLiteral("ButtonGetDoc"));
        QSizePolicy sizePolicy(QSizePolicy::Fixed, QSizePolicy::Fixed);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(ButtonGetDoc->sizePolicy().hasHeightForWidth());
        ButtonGetDoc->setSizePolicy(sizePolicy);

        gridLayout->addWidget(ButtonGetDoc, 0, 1, 1, 1, Qt::AlignHCenter);

        TextboxDocContent = new QPlainTextEdit(centralWidget);
        TextboxDocContent->setObjectName(QStringLiteral("TextboxDocContent"));
        TextboxDocContent->setEnabled(true);
        QSizePolicy sizePolicy1(QSizePolicy::Expanding, QSizePolicy::Expanding);
        sizePolicy1.setHorizontalStretch(0);
        sizePolicy1.setVerticalStretch(0);
        sizePolicy1.setHeightForWidth(TextboxDocContent->sizePolicy().hasHeightForWidth());
        TextboxDocContent->setSizePolicy(sizePolicy1);
        TextboxDocContent->setMinimumSize(QSize(360, 200));
        TextboxDocContent->setReadOnly(true);

        gridLayout->addWidget(TextboxDocContent, 2, 0, 1, 3);

        InputDocName = new QLineEdit(centralWidget);
        InputDocName->setObjectName(QStringLiteral("InputDocName"));
        sizePolicy.setHeightForWidth(InputDocName->sizePolicy().hasHeightForWidth());
        InputDocName->setSizePolicy(sizePolicy);

        gridLayout->addWidget(InputDocName, 0, 2, 1, 1, Qt::AlignRight);

        ButtonCreate = new QPushButton(centralWidget);
        ButtonCreate->setObjectName(QStringLiteral("ButtonCreate"));
        sizePolicy.setHeightForWidth(ButtonCreate->sizePolicy().hasHeightForWidth());
        ButtonCreate->setSizePolicy(sizePolicy);

        gridLayout->addWidget(ButtonCreate, 0, 0, 1, 1, Qt::AlignLeft);

        MainWindow->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(MainWindow);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 378, 20));
        MainWindow->setMenuBar(menuBar);
        mainToolBar = new QToolBar(MainWindow);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        MainWindow->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(MainWindow);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        MainWindow->setStatusBar(statusBar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QApplication::translate("MainWindow", "MainWindow", 0));
        ButtonGetDoc->setText(QApplication::translate("MainWindow", "Get document", 0));
        TextboxDocContent->setPlainText(QString());
        ButtonCreate->setText(QApplication::translate("MainWindow", "Create new", 0));
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
